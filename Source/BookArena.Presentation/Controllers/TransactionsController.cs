using System.Linq;
using System.Web.Http;
using BookArena.App.Helper;
using BookArena.App.ViewModels;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.App.Controllers
{
    [Authorize]
    public class TransactionsController : BaseApiController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ModelFactory _modelFactory;

        public TransactionsController(IBookRepository bookRepository, IStudentRepository studentRepository,
            ITransactionRepository transactionRepository, ModelFactory modelFactory)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _transactionRepository = transactionRepository;
            _modelFactory = modelFactory;
        }

        public IHttpActionResult Get()
        {
            var data =
                Mapper<Transaction, TransactionViewModel>.ListMap(
                    _transactionRepository.FindAll().OrderByDescending(x => x.Id).ToList());
            return Ok(data);
        }

        public IHttpActionResult Get(int id)
        {
            var transaction =
                Mapper<Transaction, TransactionViewModel>.SingleMap(_transactionRepository.Find(x => x.Id == id));

            if (transaction == null) return BadRequest("Transaction not found!");

            transaction.Book = _modelFactory.Create(_bookRepository.Find(x => x.Id == transaction.BookId));
            transaction.Student = _studentRepository.Find(x => x.Id == transaction.StudentId);
            return Ok(transaction);
        }

        public IHttpActionResult Put(int id)
        {
            var transaction = _transactionRepository.Find(x => x.Id == id && x.IsActive);

            if (transaction == null)
            {
                return BadRequest("This transaction is already cleared!");
            }

            var bookMetaData = _bookRepository.BookMetaData(x => x.UniqueKey == transaction.BookUniqueKey);
            bookMetaData.IsAvailable = true;
            _bookRepository.InsertOrUpdateMetaData(bookMetaData);

            transaction.Status = "Returned";
            transaction.IsActive = false;
            _transactionRepository.Update(transaction);

            _bookRepository.Save();
            _transactionRepository.Save();

            return Ok(new
            {
                Data = transaction,
                Message = "The transaction is cleared!"
            });
        }
    }
}