using System.Linq;
using System.Web.Http;
using BookArena.App.Helper;
using BookArena.App.ViewModels;
using BookArena.Core;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.App.Controllers
{
    [Authorize]
    public class StudentsController : BaseApiController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;

        public StudentsController(IStudentRepository studentRepository, ITransactionRepository transactionRepository)
        {
            _studentRepository = studentRepository;
            _transactionRepository = transactionRepository;
        }

        public IHttpActionResult Get(int? page, int pageSize = 10)
        {
            var query = _studentRepository.FindAll().OrderBy(x => x.Id);
            var model = Pagination<Student>.GetPagedData(query, page, pageSize);

            return Ok(model);
        }

        public IHttpActionResult Get(int id)
        {
            var model = _studentRepository.Find(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            var transactions = _transactionRepository.FindAll(x => x.StudentId == id).ToList();

            var student = Mapper<Student, StudentViewModel>.SingleMap(model);
            student.Transactions = Mapper<Transaction, TransactionViewModel>.ListMap(transactions);

            return Ok(student);
        }

        [Route("api/students/search/{idCard}")]
        public IHttpActionResult Get(string idCard)
        {
            var model = _studentRepository.Find(x => x.IdCardNumber == idCard);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        public IHttpActionResult Post(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var duplicate = _studentRepository.Find(x => x.IdCardNumber == student.IdCardNumber);
            if (duplicate != null)
            {
                return BadRequest("The ID Card number is already exist in the record. Please check again.");
            }

            _studentRepository.Insert(student);
            _studentRepository.Save();

            return Ok(student);
        }

        public IHttpActionResult Put(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var duplicate = _studentRepository.Find(x => x.IdCardNumber == student.IdCardNumber && x.Id != student.Id);

            if (duplicate != null)
            {
                return BadRequest("The ID Card number is already exist in the record. Please check again.");
            }
            _studentRepository.Update(student);
            _studentRepository.Save();

            return Ok();
        }
    }
}