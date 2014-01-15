using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;

namespace BookArena.Web.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController()
        {
            _bookRepository = new BookRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            var model = _bookRepository.GetAll();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}