using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Model;
using BookArena.Model.EntityModel;
using BookArena.Web.Helper;

namespace BookArena.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IBookRepository _bookRepository;

        public StudentsController()
        {
            _studentRepository = new StudentRepository();
            _bookRepository = new BookRepository();
        }

        public StudentsController(IStudentRepository studentRepository, IBookRepository bookRepository)
        {
            _studentRepository = studentRepository;
            _bookRepository = bookRepository;
        }

        public JsonResult Index(int? page)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
            const int pageSize = 10;
            var model = GetPagedStudents((page ?? 0)*pageSize, pageSize);

            return Json(new
            {
                Data = model,
                CurrentPage = (page ?? 0)
            }, JsonRequestBehavior.AllowGet);
        }

        private PagedList<Student> GetPagedStudents(int skip, int take)
        {
            var query = _studentRepository.FindAll().OrderBy(x => x.Id);
            var studentCount = query.Count();
            var students = query.Skip(skip).Take(take).ToList();
            return new PagedList<Student>
            {
                Entities = students,
                HasNext = (skip + 10 < studentCount),
                HasPrevious = (skip > 0)
            };
        }

        public JsonResult Student(int id)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
            var model = _studentRepository.Find(x => x.Id == id);
            var transactions = _bookRepository.Transactions(x => x.StudentId == id);
            return Json(new {Data = model, Transactions = transactions}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentByIdCard(string idCard)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
            var model = _studentRepository.Find(x => x.IdCardNumber == idCard);
            return Json(new {Data = model}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Student student)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse());
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid student information!"
                    }
                });
            }
            var duplicate = _studentRepository.FindAll().FirstOrDefault(x => x.IdCardNumber == student.IdCardNumber);
            if (duplicate != null)
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The ID Card number is already exist in the record. Please check again."
                    }
                });
            _studentRepository.InsertOrUpdate(student);
            _studentRepository.Save();
            return Json(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Student registered successfully."
                }
            });
        }

        [HttpPost]
        public JsonResult Edit(Student student)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse());
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid student information!"
                    }
                });
            }
            var duplicate =
                _studentRepository.FindAll()
                    .FirstOrDefault(x => x.IdCardNumber == student.IdCardNumber && x.Id != student.Id);
            if (duplicate != null)
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The ID Card number is already exist in the record. Please check again."
                    }
                });
            _studentRepository.InsertOrUpdate(student);
            _studentRepository.Save();
            return Json(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Student updated successfully."
                }
            });
        }
    }
}