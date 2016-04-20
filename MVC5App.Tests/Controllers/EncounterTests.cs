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


        [SetUp]
        public void Init()
        {
            _encounterMock = new Mock<IEncounterService>();
            _dataMock = new Mock<ITableDataService>();
        }


        [Test]
        public void CreateADeadlyEncounterForAParty()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            var mockMonsters = Generator.CreateMonsters(15);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters,
                Party = mockParty
            });

            var deadlyDifficulty = _encounterMock.Object.Encounter.Party.Difficulty.Sum(xp => xp.Deadly);
            var encounterXp = _encounterMock.Object.Encounter.ExperienceValue();

            Assert.IsTrue(encounterXp >= deadlyDifficulty );
        }

        [Test]
        public void CreateAHardEncounterForAParty()
        {
            var levels = new List<int> { 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            var mockMonsters = Generator.CreateMonsters(8);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters,
                Party = mockParty
            });

            var mediumDifficulty = _encounterMock.Object.Encounter.Party.Difficulty.Sum(xp => xp.Medium);
            var deadlyDifficulty = _encounterMock.Object.Encounter.Party.Difficulty.Sum(xp => xp.Deadly);
            var encounterXp = _encounterMock.Object.Encounter.ExperienceValue();

            Assert.IsTrue(encounterXp >= mediumDifficulty && encounterXp < deadlyDifficulty);
        }

        [Test]
        public void CreateAMediumEncounterForAParty()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            var mockMonsters = Generator.CreateMonsters(5);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters,
                Party = mockParty
            });

            var easyDifficulty = _encounterMock.Object.Encounter.Party.Difficulty.Sum(xp => xp.Easy);
            var hardDifficulty = _encounterMock.Object.Encounter.Party.Difficulty.Sum(xp => xp.Hard);
            var encounterXp = _encounterMock.Object.Encounter.ExperienceValue();

            Assert.IsTrue(encounterXp >= easyDifficulty && encounterXp < hardDifficulty);
        }

        [Test]
        public void CreateAnEasyEncounterForAParty()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            var mockMonsters = Generator.CreateMonsters(1);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters,
                Party = mockParty
            });

            var mediumDifficulty = _encounterMock.Object.Encounter.Party.Difficulty.Sum(xp => xp.Medium);
            var encounterXp = _encounterMock.Object.Encounter.ExperienceValue();

            Assert.IsTrue(encounterXp < mediumDifficulty);
        }


        [Test]
        public void GetMonsterViewModel()
        {
            var mockMonster = Generator.CreateMonsters(0);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
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