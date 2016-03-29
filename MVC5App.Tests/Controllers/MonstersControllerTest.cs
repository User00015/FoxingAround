using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Services;
using Ninject;
using Ninject.MockingKernel.Moq;

namespace MVC5App.Tests.Controllers
{
    [TestClass]
    public class MonstersControllerTest
    {
        private Mock<IEncounterService> _encounterMock;
        private Mock<ITableDataService> _dataMock;

        [TestInitialize]
        public void Initialize()
        {
            _encounterMock = new Mock<IEncounterService>();
            _encounterMock.Setup(mock => mock.GetMonster()).Returns(true);

            _dataMock = new Mock<ITableDataService>();
        }


        [TestMethod]
        public void Index()
        {
            var encounter = new MonstersController(_dataMock.Object, _encounterMock.Object);
            var test = encounter.Foo();

            Assert.IsTrue(test);
        }

    }
}
