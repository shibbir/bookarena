using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;

namespace BookArena.Web.Controllers
{
    public class ApiController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;

        public ApiController()
        {
            _bookRepository = new BookRepository();
            _studentRepository = new StudentRepository();
        }

        [HttpGet]
        public JsonResult Books()
        {
            var model = _bookRepository.GetAll();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Book(int id)
        {
            var model = _bookRepository.GetById(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Students()
        {
            var model = _studentRepository.GetAll();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Student(int id)
        {
            var model = _studentRepository.GetById(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}