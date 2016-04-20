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
    public class MonsterTests
    {
        private Mock<IEncounterService> _encounterMock;
        private Mock<ITableDataService> _dataMock;
        private Mock<IEncounterVM> _monsterMock;
        private Mock<IPartyViewModel> _party;

        [SetUp]
        public void Init()
        {
            _encounterMock = new Mock<IEncounterService>();
            _dataMock = new Mock<ITableDataService>();
            _monsterMock = new Mock<IEncounterVM>();
            _party = new Mock<IPartyViewModel>();
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1.5)]
        [TestCase(3, 2)]
        [TestCase(7, 2.5)]
        [TestCase(11, 3)]
        [TestCase(15, 4)]
        public void GetEncounterSizeMultiplier(int numberOfMonsters, double expectedMultiplier)
        {
            var mockMonsters = Generator.CreateMonsters(numberOfMonsters);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            Assert.IsTrue(_encounterMock.Object.Encounter.GetMonsterSizeMultiplier() == expectedMultiplier);
        }

        [Test]
        public void GetSingleMonsterEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(1);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            //Total XP * 1
            Assert.IsTrue(_encounterMock.Object.Encounter.ExperienceValue() == 50);

        }

        [Test]
        public void GetAPairOfMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(2);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            //Total XP * 1.5
            Assert.IsTrue(_encounterMock.Object.Encounter.ExperienceValue() == (int)(50 * 2 * 1.5));
        }

        [Test]
        public void GetAGroupOfThreeToSixMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(3);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            //Total XP * 2
            Assert.IsTrue(_encounterMock.Object.Encounter.ExperienceValue() == 50 * 3 * 2);
        }

        [Test]
        public void GetAGangOfSevenToTenMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(7);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            //Total XP * 2.5
            Assert.IsTrue(_encounterMock.Object.Encounter.ExperienceValue() == (int)(50 * 7 * 2.5));
        }

        [Test]
        public void GetAMobOfElevenToFourteenMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(11);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            //Total XP * 3
            Assert.IsTrue(_encounterMock.Object.Encounter.ExperienceValue() == 50 * 11 * 3);
        }

        [Test]
        public void GetAHordeOfFifteenOrMoreMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(15);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterVM
            {
                Monsters = mockMonsters
            });

            //Total XP * 4
            Assert.IsTrue(_encounterMock.Object.Encounter.ExperienceValue() == 50 * 15 * 4);
        }


    } //Bottom
}
