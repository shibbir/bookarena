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
            return Json(new
            {
                Data = model,
                Response = new Response
                {
                    ResponseType = "Success",
                    Message = "Books data fetched successfully!"
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Book(int id)
        {
            var model = _bookRepository.GetById(id);
            return Json(new
            {
                Data = model,
                Response = new Response
                {
                    ResponseType = "Success",
                    Message = "Data fetched successfully!"
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Students()
        {
            var model = _studentRepository.GetAll();
            return Json(new
            {
                Data = model,
                Response = new Response
                {
                    ResponseType = "Success",
                    Message = "Students data fetched successfully!"
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Student(int id)
        {
            var model = _studentRepository.GetById(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("api/students/add")]
        public JsonResult StudentAdd(Student student)
        {
            if (_studentRepository.Create(student))
            {
                return Json(new Response
                {
                    ResponseType = "Success",
                    Message = "Registration is successfull!"
                });
            }
            return Json(new Response
            {
                ResponseType = "Error",
                Message = "Oops! Something happend. Please try again."
            });
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
            return Json(Session["IsActive"] == null ? null : _accountRepository.User(), JsonRequestBehavior.AllowGet);
        }
    }

    public class Response
    {
        public string ResponseType { get; set; }
        public string Message { get; set; }
    }
}