using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using BookArena.App.Helper;
using BookArena.App.ViewModels;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.App.Controllers
{
    [Authorize]
    public class BooksController : BaseApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ModelFactory _modelFactory;

        public BooksController(IBookRepository bookRepository,
            IStudentRepository studentRepository,
            ICategoryRepository categoryRepository, ITransactionRepository transactionRepository,
            ModelFactory modelFactory)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
            _modelFactory = modelFactory;
        }

        [AllowAnonymous]
        public IHttpActionResult Get(bool includeCategory = false)
        {
            if (includeCategory)
            {
                var model = _categoryRepository.FindAll().ToList().Select(category => _modelFactory.Create(category));
                return Ok(model);
            }

            var books =
                _bookRepository.FindAll().OrderByDescending(x => x.Id).Take(5).Select(book => new BasicBookViewModel
                {
                    BookId = book.Id,
                    Title = book.Title,
                    ImageFileName = book.ImageFileName
                }).ToList();
            foreach (var book in books)
            {
                book.AvailableQuantity = _bookRepository.AvailableBooks(book.BookId);
            }
            return Ok(books);
        }

        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var model = _modelFactory.Create(_bookRepository.Find(x => x.Id == id));
            model.AvailableQuantity = _bookRepository.AvailableBooks(model.Id);
            return Ok(model);
        }

        public IHttpActionResult Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var duplicate = _bookRepository.Find(x => x.Title == book.Title);
            if (duplicate != null)
            {
                return BadRequest("The title is already exists!");
            }

            _bookRepository.Insert(book);
            _bookRepository.Save();

            for (var i = 0; i < book.Quantity; i++)
            {
                var uniqueKey = "ISBN " + new Random().Next(1000, 9000).ToString(CultureInfo.InvariantCulture);
                var key = uniqueKey;
                var duplicateKey = _bookRepository.BookMetaData(x => x.UniqueKey == key);
                while (duplicateKey != null)
                {
                    uniqueKey = "ISBN " + new Random().Next(1000, 9000).ToString(CultureInfo.InvariantCulture);
                    var key1 = uniqueKey;
                    duplicateKey = _bookRepository.BookMetaData(x => x.UniqueKey == key1);
                }
                _bookRepository.InsertOrUpdateMetaData(new BookMetaData
                {
                    BookId = book.Id,
                    IsAvailable = true,
                    UniqueKey = uniqueKey
                });
            }
            _bookRepository.Save();
            return Ok();
        }

        public IHttpActionResult Put(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var duplicate = _bookRepository.Find(x => x.Title == book.Title && x.Id != book.Id);

            if (duplicate != null)
            {
                return BadRequest("The title is already exists for another book!");
            }
            _bookRepository.Update(book);
            _bookRepository.Save();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Borrow(int studentId, int bookId)
        {
            var availablebook = _bookRepository.BookMetaData(x => x.BookId == bookId && x.IsAvailable);
            if (availablebook == null)
            {
                return BadRequest("Sorry. This book is not available now.");
            }
            var student = _studentRepository.Find(x => x.Id == studentId);
            if (student == null)
            {
                return BadRequest("Invalid student information.");
            }
            var borrowerActiveTransactions =
                _transactionRepository.FindAll(x => x.StudentId == studentId && x.IsActive).ToList();
            if (borrowerActiveTransactions.Count() >= 2)
            {
                return BadRequest("This student already have two borrowed books.");
            }
            if (borrowerActiveTransactions.Any(transaction => transaction.BookId == bookId && transaction.IsActive))
            {
                return BadRequest("This student already borrowed this book.");
            }
            _transactionRepository.Insert(new Transaction
            {
                BookId = bookId,
                BookUniqueKey = availablebook.UniqueKey,
                StudentId = studentId,
                Status = "Not returned yet",
                IsActive = true
            });
            availablebook.IsAvailable = false;
            _bookRepository.InsertOrUpdateMetaData(availablebook);

            _bookRepository.Save();
            _transactionRepository.Save();

            return Ok(new
            {
                Data = _bookRepository.AvailableBooks(bookId),
                Message = "Operation Successfull."
            });
        }
    }
}