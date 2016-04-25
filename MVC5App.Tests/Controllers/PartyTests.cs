using System.Collections.Generic;
using System.Linq;
using Moq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using NUnit.Framework;

namespace MVC5App.Tests.Controllers
{
    [TestFixture]
    public class PartyTests
    {
        private IEncounterService _encounterService;

        [SetUp]
        public void Init()
        {
            _encounterService = new EncounterService();
            _encounterService.CreateEncounter(new PartyViewModel()
            {
                PartyLevel = 3,
                PartySize = 6
            });

        }

        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAnEasyEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.TotalEasyXP == 75 * 6);
        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAMediumEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.TotalMediumXP == 150 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAHardEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.TotalHardXP == 225 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForADeadlyEncounter()
        {
            Assert.IsTrue(_encounterService.Encounter.Party.TotalDeadlyXP == 400 * 6);

        }


    } //Bottom
}
