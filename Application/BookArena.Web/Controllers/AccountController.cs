using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.DAL.Repository;
using BookArena.Model;

namespace BookArena.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController()
        {
            _accountRepository = new AccountRepository();
        }

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public JsonResult Index()
        {
            return Json(Session["IsActive"] == null ? null : _accountRepository.User(), JsonRequestBehavior.AllowGet);
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
            return Json(new {Data = model});
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
    }
}