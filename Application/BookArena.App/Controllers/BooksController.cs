using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
                    ImageFilePath = "/Content/Images/" + book.ImageFileName
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
            if (model == null)
            {
                return NotFound();
            }
            model.AvailableQuantity = _bookRepository.AvailableBooks(model.Id);
            return Ok(model);
        }

        public async Task<IHttpActionResult> Post()
        {
            var imageFileName = string.Empty;

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            if (HttpContext.Current.Request.Files.Count != 0)
            {
                var file = HttpContext.Current.Request.Files[0];
                file.SaveAs(
                    HttpContext.Current.Server.MapPath("~/Content/Images/" + file.FileName));
                imageFileName = file.FileName;

                ClearBodyPartFiles();
            }

            var book = new Book
            {
                Title = result.FormData["Title"],
                Author = result.FormData["Author"],
                LongDescription = result.FormData["LongDescription"],
                ShortDescription = result.FormData["ShortDescription"],
                Quantity = int.Parse(result.FormData["Quantity"]),
                CategoryId = int.Parse(result.FormData["CategoryId"]),
                Rating = double.Parse(result.FormData["Rating"]),
                ImageFileName = imageFileName
            };

            if (_bookRepository.Find(x => x.Title == book.Title) != null)
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

        [HttpPost]
        [Route("api/Books/Upload")]
        public async Task<IHttpActionResult> Upload()
        {
            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            if (HttpContext.Current.Request.Files.Count == 0) return BadRequest();

            var file = HttpContext.Current.Request.Files[0];
            file.SaveAs(
                HttpContext.Current.Server.MapPath("~/Content/Images/" + file.FileName));

            var bookId = int.Parse(result.FormData["Id"]);
            var book = _bookRepository.Find(x => x.Id == bookId);
            if (File.Exists(HttpContext.Current.Server.MapPath("~/Content/Images/" + book.ImageFileName)))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/Content/Images/" + book.ImageFileName));
            }
            book.ImageFileName = file.FileName;

            _bookRepository.Update(book);
            _bookRepository.Save();

            ClearBodyPartFiles();

            return Ok(_modelFactory.Create(book));
        }

        private static MultipartFormDataStreamProvider GetMultipartProvider()
        {
            const string uploadFolder = "~/Content/Images";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);

            return new MultipartFormDataStreamProvider(root);
        }

        private static void ClearBodyPartFiles()
        {
            var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Content/Images"),
                @"*BodyPart_*");

            foreach (var file in files.Where(File.Exists))
            {
                File.Delete(file);
            }
        }
    }
}