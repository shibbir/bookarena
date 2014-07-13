using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.Model;
using BookArena.Model.EntityModels;
using BookArena.Web.Helper;
using Newtonsoft.Json;

namespace BookArena.Web.Controllers
{
    public class BooksController : Controller
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

        public ActionResult Index()
        {
            var model = _categoryRepository.FindAll().ToList().Select(category => _modelFactory.Create(category));
            return Content(JsonConvert.SerializeObject(new {Data = model}), "application/json");
        }

        public ActionResult Latest()
        {
            var books = _bookRepository.Books().OrderByDescending(x => x.BookId).Take(5).ToList();
            foreach (var book in books)
            {
                book.AvailableQuantity = _bookRepository.AvailableBooks(book.BookId);
            }
            return Content(JsonConvert.SerializeObject(new {Data = books}), "application/json");
        }

        public ActionResult Book(int id)
        {
            var model = _bookRepository.Book(x => x.BookId == id);
            return Content(JsonConvert.SerializeObject(new {Data = model}), "application/json");
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            if (!Request.IsAuthenticated) return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid book information!"
                    }
                }), "application/json");
            }
            var duplicate = _bookRepository.Book(x => x.Title == book.Title);
            if (duplicate != null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The title is already exists!"
                    }
                }), "application/json");
            }
            _bookRepository.InsertOrUpdate(book);
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
                    BookId = book.BookId,
                    IsAvailable = true,
                    UniqueKey = uniqueKey
                });
            }
            _bookRepository.Save();
            return Content(JsonConvert.SerializeObject(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Book uploaded successfully!"
                }
            }), "application/json");
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (!Request.IsAuthenticated) return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid book information!"
                    }
                }), "application/json");
            }
            var duplicate = _bookRepository.Book(x => x.Title == book.Title && x.BookId != book.BookId);
            if (duplicate != null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The title is already exists for another book!"
                    }
                }), "application/json");
            }
            _bookRepository.InsertOrUpdate(book);
            _bookRepository.Save();
            return Content(JsonConvert.SerializeObject(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Book updated successfully!"
                }
            }), "application/json");
        }

        public ActionResult Category(int id)
        {
            var model =
                _categoryRepository.FindAll()
                    .Where(x => x.CategoryId == id)
                    .ToList()
                    .Select(category => _modelFactory.Create(category));
            return Content(JsonConvert.SerializeObject(new {Data = model}), "application/json");
        }

        [HttpPost]
        public ActionResult Borrow(int studentId, int bookId)
        {
            if (!Request.IsAuthenticated) return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            var availablebook = _bookRepository.BookMetaData(x => x.BookId == bookId && x.IsAvailable);
            if (availablebook == null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Sorry. This book is not available now."
                    }
                }), "application/json");
            }
            var student = _studentRepository.Find(x => x.Id == studentId);
            if (student == null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid student information."
                    }
                }), "application/json");
            }
            var borrowerActiveTransactions =
                _transactionRepository.FindAll(x => x.StudentId == studentId && x.IsActive).ToList();
            if (borrowerActiveTransactions.Count() >= 2)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "This student already have two borrowed books."
                    }
                }), "application/json");
            }
            if (borrowerActiveTransactions.Any(transaction => transaction.BookId == bookId && transaction.IsActive))
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "This student already borrowed this book."
                    }
                }), "application/json");
            }
            _transactionRepository.InsertOrUpdate(new Transaction
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

            return Content(JsonConvert.SerializeObject(new
            {
                Data = _bookRepository.AvailableBooks(bookId),
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Operation Successfull."
                }
            }), "application/json");
        }

        [HttpPost]
        public ActionResult Receive(int id)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");

            var transaction = _transactionRepository.Find(x => x.Id == id && x.IsActive);

            if (transaction == null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "This transaction is already cleared!"
                    }
                }), "application/json");
            }

            var bookMetaData = _bookRepository.BookMetaData(x => x.UniqueKey == transaction.BookUniqueKey);
            bookMetaData.IsAvailable = true;
            _bookRepository.InsertOrUpdateMetaData(bookMetaData);

            transaction.Status = "Returned";
            transaction.IsActive = false;
            _transactionRepository.InsertOrUpdate(transaction);

            _bookRepository.Save();
            _transactionRepository.Save();

            return Content(JsonConvert.SerializeObject(new
            {
                Data = transaction,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "The transaction is cleared!"
                }
            }), "application/json");
        }
    }
}