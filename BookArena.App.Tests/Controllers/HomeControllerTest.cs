using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookArena.App.Controllers;

namespace BookArena.App.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}