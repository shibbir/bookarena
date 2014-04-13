using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.Model;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;
using BookArena.Web.Helper;
using Newtonsoft.Json;

namespace BookArena.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;

        public StudentsController(IStudentRepository studentRepository, ITransactionRepository transactionRepository)
        {
            _studentRepository = studentRepository;
            _transactionRepository = transactionRepository;
        }

        public ActionResult Index(int? page)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            const int pageSize = 10;
            var model = GetPagedStudents((page ?? 0)*pageSize, pageSize);

            return Content(JsonConvert.SerializeObject(new
            {
                Data = model,
                CurrentPage = (page ?? 0)
            }), "application/json");
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

        public ActionResult Student(int id)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            var model = _studentRepository.Find(x => x.Id == id);
            var transactions =
                Mapper<Transaction, TransactionViewModel>.ListMap(
                    _transactionRepository.FindAll(x => x.StudentId == id).ToList());
            return Content(JsonConvert.SerializeObject(new
            {
                Data = model,
                Transactions = transactions
            }), "application/json");
        }

        public ActionResult StudentByIdCard(string idCard)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
            var model = _studentRepository.Find(x => x.IdCardNumber == idCard);
            return Content(JsonConvert.SerializeObject(new {Data = model}), "application/json");
        }

        [HttpPost]
        public ActionResult Add(Student student)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid student information!"
                    }
                }), "application/json");
            }
            var duplicate = _studentRepository.Find(x => x.IdCardNumber == student.IdCardNumber);
            if (duplicate != null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The ID Card number is already exist in the record. Please check again."
                    }
                }), "application/json");
            }
            _studentRepository.InsertOrUpdate(student);
            _studentRepository.Save();
            return Content(JsonConvert.SerializeObject(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Student registered successfully."
                }
            }), "application/json");
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid student information!"
                    }
                }), "application/json");
            }
            var duplicate = _studentRepository.Find(x => x.IdCardNumber == student.IdCardNumber && x.Id != student.Id);
            if (duplicate != null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The ID Card number is already exist in the record. Please check again."
                    }
                }), "application/json");
            }
            _studentRepository.InsertOrUpdate(student);
            _studentRepository.Save();
            return Content(JsonConvert.SerializeObject(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Student updated successfully."
                }
            }), "application/json");
        }
    }
}