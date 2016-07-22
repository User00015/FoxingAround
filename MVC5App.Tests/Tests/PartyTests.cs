using Moq;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.Services.Models;
using MVC5App.ViewModels;
using NUnit.Framework;

namespace MVC5App.Tests.Controllers
{
    [TestFixture]
    public class PartyTests
    {
        private IEncounterService _encounterService;
        private Mock<IMonsterRepository> _monsterRepositoryMock;
        private PartyViewModel _party;
        private Difficulty _difficulty;

        [SetUp]
        public void Init()
        {
            _monsterRepositoryMock = new Mock<IMonsterRepository>();
            _encounterService = new EncounterService(_monsterRepositoryMock.Object);
            _encounterService.CreateEncounter(new PartyViewModel()
            {
                PartyLevel = 3,
                PartySize = 6
            });

            _party = new PartyViewModel
            {
                PartyLevel = 3,
                PartySize = 6,
                Difficulty = 3
            };
            _difficulty = new Difficulty(_party.PartyLevel);
        }

        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAnEasyEncounter()
        {
            Assert.IsTrue(_difficulty.Easy * 6 == 75 * 6);
        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAMediumEncounter()
        {
            Assert.IsTrue(_difficulty.Medium * 6 == 150 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAHardEncounter()
        {
            Assert.IsTrue(_difficulty.Hard * 6 == 225 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForADeadlyEncounter()
        {
            Assert.IsTrue(_difficulty.Deadly * 6 == 400 * 6);

        }


    } //Bottom
}
