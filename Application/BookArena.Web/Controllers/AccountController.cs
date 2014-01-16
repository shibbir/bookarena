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

        [HttpGet]
        public JsonResult Get(int id)
        {
            var model = _accountRepository.User(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Login(ApplicationUser applicationUser)
        {
            return Json(_accountRepository.Login(applicationUser), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAuthenticated()
        {
            return Json(_accountRepository.IsUserAuthenticated(), JsonRequestBehavior.AllowGet);
        }
    }
}