using System.Collections.Generic;
using System.Linq;
using Moq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.Services.Models;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;
using NUnit.Framework;

namespace MVC5App.Tests.Controllers
{
    [TestFixture]
    public class EncounterTests
    {
        private Mock<IEncounterService> _encounterMock;
        private Mock<ITableDataService> _dataMock;
        private Mock<IMonsterRepository> _mockMonsterRepository;
        private Mock<IEncounterViewModel> _mockEncounterViewModel;
        private IPartyViewModel _party;


        [SetUp]
        public void Init()
        {
            _encounterMock = new Mock<IEncounterService>();
            _dataMock = new Mock<ITableDataService>();
            _mockMonsterRepository = new Mock<IMonsterRepository>();
            _mockEncounterViewModel =  new Mock<IEncounterViewModel>();
            _party = new PartyViewModel()
            {
                PartyLevel = 3,
                PartySize = 6
            };

            //_encounterMock.CreateEncounter(_party);
            _encounterMock.Setup(mock => mock.Encounter).Returns(() => new EncounterViewModel()
            {
                Monsters = Generator.CreateMonsters(6),
                Party = new Party(_party)
            });
        }


        [Test]
        public void CreateADeadlyEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(2440);

            var deadlyDifficulty = _encounterMock.Object.Encounter.Party.GetDifficulty((int)Difficulty.DifficultyEnum.Deadly);
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp >= deadlyDifficulty );
        }

        [Test]
        public void CreateAHardEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(1440);

            var mediumDifficulty =
                _encounterMock.Object.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Medium);
            var deadlyDifficulty = _encounterMock.Object.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Deadly);
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp >= mediumDifficulty && encounterXp < deadlyDifficulty);
        }

        [Test]
        public void CreateAMediumEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(900);

            var easyDifficulty = _encounterMock.Object.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Easy);
            var hardDifficulty = _encounterMock.Object.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Hard);
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp >= easyDifficulty && encounterXp < hardDifficulty);
        }

        [Test]
        public void CreateAnEasyEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(750);

            var mediumDifficulty = _encounterMock.Object.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Medium);
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp < mediumDifficulty);
        }

        [Test]
        public void CreateToEasyOfAnEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(0);

            var easyDifficulty = _encounterMock.Object.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Easy);
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp < easyDifficulty);
        }


        [Test]
        public void GetMonsterViewModel()
        {
            var mockMonster = Generator.CreateMonsters(0);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterViewModel
            {
                Monsters = mockMonster
            });

            var encounter = new MonstersController(_dataMock.Object, _encounterMock.Object);
            var test = encounter.Encounter().Monsters.ToList();

            Assert.IsTrue(test.Count == 0);
            Assert.IsTrue(test.Any() == false);
        }

    } //Bottom
}