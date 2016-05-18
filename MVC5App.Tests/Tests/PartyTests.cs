using System.Collections.Generic;
using System.Linq;
using Moq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Repositories;
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

        }

        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAnEasyEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Easy) == 75 * 6);
        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAMediumEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Medium) == 150 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAHardEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Hard) == 225 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForADeadlyEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.GetDifficulty((int) Difficulty.DifficultyEnum.Deadly) == 400 * 6);

        }


    } //Bottom
}
