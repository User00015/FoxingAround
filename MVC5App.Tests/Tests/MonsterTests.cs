using System.Linq;
using Moq;
using MVC5App.DynamoDb;
using MVC5App.Repositories;
using MVC5App.Services;
using MVC5App.ViewModels;
using NUnit.Framework;

namespace MVC5App.Tests.Controllers
{
    [TestFixture]
    public class MonsterTests
    {
        private Mock<ITableDataService> _databaseMock;
        private MonsterRepository _mockRepository;
        private EncounterService _mock;

        [SetUp]
        public void Init()
        {
            _databaseMock = new Mock<ITableDataService>();
            _mockRepository = new MonsterRepository(_databaseMock.Object);
            _mock = new EncounterService(_mockRepository);
            _mock.CreateRandomEncounter(new PartyViewModel());

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
            _mock.Encounter.Monsters = mockMonsters;

            Assert.IsTrue(_mock.ApplyMonsterSizeMultiplier(_mock.Encounter.Monsters.Sum(p => p.Quantity)) == expectedMultiplier);
        }

        [Test]
        public void GetSingleMonsterEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(1);
            _mock.Encounter.Monsters = mockMonsters;

            //Total XP * 1
            Assert.IsTrue(_mock.GetEncountersExperienceValue(_mock.Encounter.Monsters) == 50);

        }

        [Test]
        public void GetAPairOfMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(2);
            _mock.Encounter.Monsters = mockMonsters;


            //Total XP * 1.5
            Assert.IsTrue(_mock.GetEncountersExperienceValue(_mock.Encounter.Monsters) == (int)(50 * 2 * 1.5));
        }

        [Test]
        public void GetAGroupOfThreeToSixMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(3);
            _mock.Encounter.Monsters = mockMonsters;

            //Total XP * 2
            Assert.IsTrue(_mock.GetEncountersExperienceValue(_mock.Encounter.Monsters) == 50 * 3 * 2);
        }

        [Test]
        public void GetAGangOfSevenToTenMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(7);
            _mock.Encounter.Monsters = mockMonsters;

            //Total XP * 2.5
            Assert.IsTrue(_mock.GetEncountersExperienceValue(_mock.Encounter.Monsters) == (int)(50 * 7 * 2.5));
        }

        [Test]
        public void GetAMobOfElevenToFourteenMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(11);
            _mock.Encounter.Monsters = mockMonsters;

            //Total XP * 3
            Assert.IsTrue(_mock.GetEncountersExperienceValue(_mock.Encounter.Monsters) == 50 * 11 * 3);
        }

        [Test]
        public void GetAHordeOfFifteenOrMoreMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(15);
            _mock.Encounter.Monsters = mockMonsters;

            //Total XP * 4
            Assert.IsTrue(_mock.GetEncountersExperienceValue(_mock.Encounter.Monsters) == 50 * 15 * 4);
        }


    } //Bottom
}
