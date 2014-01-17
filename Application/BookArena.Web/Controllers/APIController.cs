using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Model;

namespace BookArena.Web.Controllers
{
    public class ApiController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;

        public ApiController()
        {
            _accountRepository = new AccountRepository();
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

        [HttpPost]
        public ActionResult Login(ApplicationUser user)
        {
            var model = _accountRepository.Login(user);
            if (model == null)
            {
                return Json(null);
            }
            Session["IsActive"] = true;
            return Json(model);
        }

        [HttpGet]
        public JsonResult Logout()
        {
            Session.RemoveAll();
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Account()
        {
            if (Session["IsActive"] == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(_accountRepository.User(), JsonRequestBehavior.AllowGet);
        }
    }
}