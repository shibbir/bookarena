using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Model;

namespace BookArena.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController()
        {
            _studentRepository = new StudentRepository();
        }

        public JsonResult Index(int? page)
        {
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
            var query = _studentRepository.All().OrderBy(x => x.Id);
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
            var model = _studentRepository.StudentDetails(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Student student)
        {
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
            var duplicateUser = _studentRepository.All().FirstOrDefault(x => x.IdCardNumber == student.IdCardNumber);
            if (duplicateUser != null)
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
    }
}