using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Model;
using BookArena.Model.EntityModel;
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

        public BooksController()
        {
            _bookRepository = new BookRepository();
            _studentRepository = new StudentRepository();
            _categoryRepository = new CategoryRepository();
            _transactionRepository = new TransactionRepository();
        }

        public BooksController(IBookRepository bookRepository, IStudentRepository studentRepository,
            ICategoryRepository categoryRepository, ITransactionRepository transactionRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
        }

        public JsonResult Index()
        {
            var model = _categoryRepository.FindAll().Select(category => new
            {
                category.CategoryId,
                category.Title,
                category.Books
            }).ToList();
            return Json(new {Data = JsonConvert.SerializeObject(model)}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Latest()
        {
            var books = _bookRepository.LatestBooks(5);
            return Json(new {Data = books}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Book(int id)
        {
            var model = _bookRepository.Book(x => x.BookId == id);
            return Json(new {Data = model}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Book book)
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse());
            if (!ModelState.IsValid)
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid book information!"
                    }
                });
            var duplicate = _bookRepository.Book(x => x.Title == book.Title);
            if (duplicate != null)
            {
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The title is already exists!"
                    }
                });
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
            return Json(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Book uploaded successfully!"
                }
            });
        }

        [HttpPost]
        public JsonResult Edit(Book book)
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse());
            if (!ModelState.IsValid)
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid book information!"
                    }
                });
            var duplicate = _bookRepository.Book(x => x.Title == book.Title && x.BookId != book.BookId);
            if (duplicate != null)
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The title is already exists!"
                    }
                });
            }

            _bookRepository.InsertOrUpdate(book);
            _bookRepository.Save();
            return Json(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Book updated successfully!"
                }
            });
        }

        public JsonResult Category(int id)
        {
            var model = _categoryRepository.FindAll().Where(x => x.CategoryId == id).Select(category => new
            {
                category.CategoryId,
                category.Title,
                category.Books
            }).ToList();
            return Json(new {Data = JsonConvert.SerializeObject(model)}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Borrow(int studentId, int bookId)
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse());
            var availablebook = _bookRepository.BookMetaData(x => x.BookId == bookId && x.IsAvailable);
            if (availablebook == null)
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Sorry. This book is not available now."
                    }
                });
            }
            var student = _studentRepository.Find(x => x.Id == studentId);
            if (student == null)
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid student information."
                    }
                });
            }
            var borrowerActiveTransactions =
                _transactionRepository.FindAll(x => x.StudentId == studentId && x.IsActive).ToList();
            if (borrowerActiveTransactions.Count() >= 2)
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "This student already have two borrowed books."
                    }
                });
            }
            if (borrowerActiveTransactions.Any(transaction => transaction.BookId == bookId && transaction.IsActive))
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "This student already borrowed this book."
                    }
                });
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

            return Json(new
            {
                Data = _bookRepository.AvailableBooks(bookId),
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Operation Successfull."
                }
            });
        }

        [HttpPost]
        public JsonResult Receive(int id)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse());

            var transaction = _transactionRepository.Find(x => x.Id == id && x.IsActive);

            if (transaction == null)
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "This transaction is already cleared!"
                    }
                });
            }

            var bookMetaData = _bookRepository.BookMetaData(x => x.UniqueKey == transaction.BookUniqueKey);
            bookMetaData.IsAvailable = true;
            _bookRepository.InsertOrUpdateMetaData(bookMetaData);

            transaction.Status = "Returned";
            transaction.IsActive = false;
            _transactionRepository.InsertOrUpdate(transaction);

            _bookRepository.Save();
            _transactionRepository.Save();

            return Json(new
            {
                Data = transaction,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "The transaction is cleared!"
                }
            });
        }
    }
}