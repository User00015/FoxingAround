using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MVC5App.Models;
using MVC5App.Repositories;
using MVC5App.Repositories.Interfaces;
using MVC5App.ViewModels;
using NUnit.Framework;

namespace MVC5App.Tests.Tests
{
    [TestFixture]
    public class MonsterDetailTests
    {
        private Mock<IMonsterRepository> _repository;

        [SetUp]
        public void Init()
        {

            _repository = new Mock<IMonsterRepository>();

            _repository.Setup(m => m.GetMonster(5)).Returns(new MonsterModel()
            {
                Name = "Detailed Test Monster",
                Id = 5,
                Xp = 100
            });
        }


        [Test]
        public void GetMonsterDetailsBasedOnId()
        {
            var details = _repository.Object.GetMonster(5);
            Assert.AreEqual(5, details.Id);
            Assert.AreEqual(100, details.Xp);

        }

        [Test]
        public void GetMonsterDetailsBasedOnBadId()
        {
            var details = _repository.Object.GetMonster(0);
            Assert.IsNull(details);
        }
    }
}
