using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Model;
using Newtonsoft.Json;

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

        public ApiController(IAccountRepository accountRepository, IBookRepository bookRepository,
            IStudentRepository studentRepository)
        {
            _accountRepository = accountRepository;
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public JsonResult Categories()
        {
            var model = _bookRepository.Categories();
            return Json(new
            {
                Data = model,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Categories data fetched successfully!"
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Books()
        {
            var model = _bookRepository.Categories();

            return Json(new
            {
                Data = model,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
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
                Data = model
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid book information!"
                    }
                });
            _bookRepository.Create(book);
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
        public JsonResult EditBook(Book book)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid book information!"
                    }
                });
            _bookRepository.Update(book);
            _bookRepository.Save();
            return Json(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Data fetched successfully!"
                }
            });
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
                    ResponseType = ResponseType.Success,
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
        public JsonResult AddStudent(Student student)
        {
            if (!ModelState.IsValid)
                return Json(new Response
                {
                    ResponseType = ResponseType.Error,
                    Message = "Invalid student information."
                });
            _studentRepository.Create(student);
            _studentRepository.Save();
            return Json(new Response
            {
                ResponseType = ResponseType.Success,
                Message = "Student registered successfully."
            });
        }

        [HttpPost]
        public ActionResult Login(ApplicationUser user)
        {
            var model = _accountRepository.Login(user);
            if (model == null)
            {
                return Json(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid username or password!"
                    }
                });
            }
            Session["IsActive"] = true;
            return Json(new
            {
                Data = new
                {
                    model.Name,
                    IsAuthenticated = true
                },
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "You have been logged in!"
                }
            });
        }

        [HttpGet]
        public JsonResult Logout()
        {
            Session.RemoveAll();
            return Json(new Response
            {
                ResponseType = ResponseType.Success,
                Message = "You have beed logged out!"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Account()
        {
            return Json(Session["IsActive"] == null ? null : _accountRepository.User(), JsonRequestBehavior.AllowGet);
        }
    }
}