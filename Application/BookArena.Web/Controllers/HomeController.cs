using System.Web.Mvc;

namespace BookArena.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("_Layout");
        }
    }
}