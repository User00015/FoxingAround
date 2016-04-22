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
    public class PartyTests
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

        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAnEasyEncounter()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterViewModel
            {
                Party = mockParty
            });
            var sum = _encounterMock.Object.Encounter.Party.TotalEasyXP;
            Assert.IsTrue(sum == 75 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAMediumEncounter()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterViewModel
            {
                Party = mockParty
            });
            var sum = _encounterMock.Object.Encounter.Party.TotalMediumXP;
            Assert.IsTrue(sum == 150 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForAHardEncounter()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterViewModel
            {
                Party = mockParty
            });
            var sum = _encounterMock.Object.Encounter.Party.TotalHardXP;
            Assert.IsTrue(sum == 225 * 6);

        }


        [Test]
        public void PartyOfSixLevelThreesAndTheirTotalXPForADeadlyEncounter()
        {
            var levels = new List<int> { 3, 3, 3, 3, 3, 3 };
            var mockParty = Generator.CreateParty(levels);

            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterViewModel
            {
                Party = mockParty
            });
            var sum = _encounterMock.Object.Encounter.Party.TotalDeadlyXP;
            Assert.IsTrue(sum == 400 * 6);

        }


    } //Bottom
}
