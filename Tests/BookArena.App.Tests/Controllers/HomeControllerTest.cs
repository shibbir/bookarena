using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookArena.App.Controllers;

namespace BookArena.App.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Should_Return_Layout()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}