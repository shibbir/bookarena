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

        public BooksController()
        {
            _bookRepository = new BookRepository();
        }

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
            var model = _bookRepository.Find(id);

            return Json(new {Data = JsonConvert.SerializeObject(model)}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Book book)
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
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
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
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
    }
}