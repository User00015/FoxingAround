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
       

        [SetUp]
        public void Init()
        {

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

            var rep = new MonsterRepository {Monsters = mockMonsters};

            Assert.IsTrue(rep.GetMonsterSizeMultiplier() == expectedMultiplier);
        }

        [Test]
        public void GetSingleMonsterEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(1);
            var rep = new MonsterRepository { Monsters = mockMonsters };

            //Total XP * 1
            Assert.IsTrue(rep.ExperienceValue() == 50);

        }

        [Test]
        public void GetAPairOfMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(2);
            var rep = new MonsterRepository { Monsters = mockMonsters };


            //Total XP * 1.5
            Assert.IsTrue(rep.ExperienceValue() == (int)(50 * 2 * 1.5));
        }

        [Test]
        public void GetAGroupOfThreeToSixMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(3);
            var rep = new MonsterRepository { Monsters = mockMonsters };


            //Total XP * 2
            Assert.IsTrue(rep.ExperienceValue() == 50 * 3 * 2);
        }

        [Test]
        public void GetAGangOfSevenToTenMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(7);
            var rep = new MonsterRepository { Monsters = mockMonsters };


            //Total XP * 2.5
            Assert.IsTrue(rep.ExperienceValue() == (int)(50 * 7 * 2.5));
        }

        [Test]
        public void GetAMobOfElevenToFourteenMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(11);
            var rep = new MonsterRepository { Monsters = mockMonsters };


            //Total XP * 3
            Assert.IsTrue(rep.ExperienceValue() == 50 * 11 * 3);
        }

        [Test]
        public void GetAHordeOfFifteenOrMoreMonstersEncounterDifficulty()
        {
            var mockMonsters = Generator.CreateMonsters(15);
            var rep = new MonsterRepository { Monsters = mockMonsters };


            //Total XP * 4
            Assert.IsTrue(rep.ExperienceValue() == 50 * 15 * 4);
        }


    } //Bottom
}
