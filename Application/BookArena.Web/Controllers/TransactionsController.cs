using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;
using BookArena.Web.Helper;
using Newtonsoft.Json;

namespace BookArena.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(IBookRepository bookRepository, IStudentRepository studentRepository,
            ITransactionRepository transactionRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _transactionRepository = transactionRepository;
        }

        public JsonResult Index()
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);
            var data =
                Mapper<Transaction, TransactionViewModel>.ListMap(
                    _transactionRepository.FindAll().OrderByDescending(x => x.Id).ToList());
            return Json(new {Data = data}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transaction(int id)
        {
            if (!Request.IsAuthenticated) return Json(Utility.AccessDeniedResponse(), JsonRequestBehavior.AllowGet);

            var transaction =
                Mapper<Transaction, TransactionViewModel>.SingleMap(_transactionRepository.Find(x => x.Id == id));

            if (transaction == null)
                return Json(new {Data = JsonConvert.SerializeObject(null)}, JsonRequestBehavior.AllowGet);
            transaction.Book = _bookRepository.Find(x => x.BookId == transaction.BookId);
            transaction.Student = _studentRepository.Find(x => x.Id == transaction.StudentId);
            return Json(new {Data = JsonConvert.SerializeObject(transaction)}, JsonRequestBehavior.AllowGet);
        }
    }
}