using System.Collections.Generic;
using System.Linq;
using Moq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Services;
using NUnit.Framework;

namespace MVC5App.Tests.Controllers
{
    [TestFixture]
    public class EncounterTests
    {
        private Mock<IEncounterService> _encounterMock;
        private Mock<ITableDataService> _dataMock;
        private IEncounterService _encounterService;
        private IPartyViewModel _party;



        [SetUp]
        public void Init()
        {
            _encounterMock = new Mock<IEncounterService>();
            _dataMock = new Mock<ITableDataService>();
            _encounterService = new EncounterService();
            _party = new PartyViewModel()
            {
                PartyLevel = 3,
                PartySize = 6
            };

            _encounterService.CreateEncounter(_party);
        }


        [Test]
        public void CreateADeadlyEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.GetEncounterExperience()).Returns(2440);

            var deadlyDifficulty = _encounterService.Encounter.Party.TotalDeadlyXP;
            var encounterXp = _encounterMock.Object.GetEncounterExperience();

            Assert.IsTrue(encounterXp >= deadlyDifficulty );
        }

        [Test]
        public void CreateAHardEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.GetEncounterExperience()).Returns(1440);

            var mediumDifficulty = _encounterService.Encounter.Party.TotalMediumXP;
            var deadlyDifficulty = _encounterService.Encounter.Party.TotalDeadlyXP;
            var encounterXp = _encounterMock.Object.GetEncounterExperience();

            Assert.IsTrue(encounterXp >= mediumDifficulty && encounterXp < deadlyDifficulty);
        }

        [Test]
        public void CreateAMediumEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.GetEncounterExperience()).Returns(900);

            var easyDifficulty = _encounterService.Encounter.Party.TotalEasyXP;
            var hardDifficulty = _encounterService.Encounter.Party.TotalHardXP;
            var encounterXp = _encounterMock.Object.GetEncounterExperience();

            Assert.IsTrue(encounterXp >= easyDifficulty && encounterXp < hardDifficulty);
        }

        [Test]
        public void CreateAnEasyEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.GetEncounterExperience()).Returns(750);

            var mediumDifficulty = _encounterService.Encounter.Party.TotalMediumXP;
            var encounterXp = _encounterMock.Object.GetEncounterExperience();

            Assert.IsTrue(encounterXp < mediumDifficulty);
        }

        [Test]
        public void CreateToEasyOfAnEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.GetEncounterExperience()).Returns(0);

            var easyDifficulty = _encounterService.Encounter.Party.TotalEasyXP;
            var encounterXp = _encounterMock.Object.GetEncounterExperience();

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
            var test = encounter.Encounter().Monsters;

            Assert.IsTrue(test.Count == 0);
            Assert.IsTrue(test.Any() == false);
        }
    } //Bottom
}