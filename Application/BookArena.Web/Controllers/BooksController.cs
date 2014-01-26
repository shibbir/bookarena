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
        private readonly IStudentRepository _studentRepository;

        public BooksController()
        {
            _bookRepository = new BookRepository();
            _studentRepository = new StudentRepository();
        }

        public BooksController(IBookRepository bookRepository, IStudentRepository studentRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
        }

        public JsonResult Index()
        {
            var model = _bookRepository.Categories().Select(category => new
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
            var model = _bookRepository.Find(x => x.BookId == id);

            return Json(new {Data = JsonConvert.SerializeObject(model)}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Book book)
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
            _bookRepository.InsertOrUpdate(book);
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

        public JsonResult Categories()
        {
            var model = _bookRepository.Categories().Select(category => new
            {
                category.CategoryId,
                category.Title
            }).ToList();

            return Json(new {Data = model}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Category(int id)
        {
            var model = _bookRepository.Categories().Where(x => x.CategoryId == id).Select(category => new
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
            var book = _bookRepository.Find(x => x.BookId == bookId);
            if (book == null || book.Quantity != 1)
            {
                return Json(new Response
                {
                    ResponseType = ResponseType.Error,
                    Message = "Sorry. This book is not available now."
                });
            }
            var student = _studentRepository.Find(x => x.Id == studentId);
            if (student == null)
            {
                return Json(new Response
                {
                    ResponseType = ResponseType.Error,
                    Message = "Invalid student information."
                });
            }
            return Json(new Response
            {
                ResponseType = ResponseType.Success,
                Message = "Operation Successfull."
            });
        }
    }
}