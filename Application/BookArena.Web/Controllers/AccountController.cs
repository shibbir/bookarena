using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BookArena.DAL;
using BookArena.Model;
using BookArena.Model.EntityModels;
using BookArena.Model.ViewModels;
using BookArena.Web.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace BookArena.Web.Controllers
{
    public class AccountController : Controller
    {
        public static BookArenaDbContext Context = new BookArenaDbContext();

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public AccountController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            }
            var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return
                Content(
                    JsonConvert.SerializeObject(
                        new {Data = Mapper<ApplicationUser, ApplicationUserViewModel>.SingleMap(user)}),
                    "application/json");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid username or password."
                    }
                }), "application/json");
            }
            var user = await UserManager.FindAsync(model.UserName, model.Password);
            if (user == null)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid username or password."
                    }
                }), "application/json");
            }
            await SignInAsync(user, model.RememberMe);

            return
                Content(
                    JsonConvert.SerializeObject(
                        new {Data = Mapper<ApplicationUser, ApplicationUserViewModel>.SingleMap(user)}),
                    "application/json");
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return Content(JsonConvert.SerializeObject(new
            {
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "You have been logged out!"
                }
            }), "application/json");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        #endregion
    }
}