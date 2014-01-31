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
    public class TransactionsController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController()
        {
            _bookRepository = new BookRepository();
            _studentRepository = new StudentRepository();
            _transactionRepository = new TransactionRepository();
        }

        public TransactionsController(IBookRepository bookRepository, IStudentRepository studentRepository,
            ITransactionRepository transactionRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _transactionRepository = transactionRepository;
        }

        public JsonResult Index()
        {
            return !Request.IsAuthenticated
                ? Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet)
                : Json(new {Data = _transactionRepository.Transactions()}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transaction(int id)
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);

            var transaction = _transactionRepository.Transactions(x => x.Id == id).FirstOrDefault();

            if (transaction != null)
            {
                transaction.Book = _bookRepository.Find(x => x.BookId == transaction.BookId);
                transaction.Student = _studentRepository.Find(x => x.Id == transaction.StudentId);
            }
            return Json(new {Data = JsonConvert.SerializeObject(transaction)}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Clear(Transaction transaction)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse());

            return Json(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "The transaction is cleared!"
                }
            });
        }
    }
}