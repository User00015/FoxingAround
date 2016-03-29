using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC5App.Controllers;

namespace MVC5App.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
