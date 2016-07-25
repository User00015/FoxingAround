﻿using System.Collections.Generic;
using System.Linq;
using Moq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
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
        private EncounterService _encounterService;
        private Difficulty _difficulty;
        private PartyService _partyService;


        [SetUp]
        public void Init()
        {
            _encounterMock = new Mock<IEncounterService>();
            _dataMock = new Mock<ITableDataService>();
            _mockEncounterViewModel = new Mock<IEncounterViewModel>();
            _mockMonsterRepository = new Mock<IMonsterRepository>();
            _mockEncounterViewModel = new Mock<IEncounterViewModel>();
            _encounterService = new EncounterService(_mockMonsterRepository.Object);
            _party = new PartyViewModel
            {
                PartyLevel = 3,
                PartySize = 6,
                Difficulty = 3
            };

            _partyService = new PartyService(_party);

            _difficulty = new Difficulty(_party.PartyLevel);
            _encounterMock.Setup(mock => mock.Encounter).Returns(() => new EncounterViewModel
            {
                Monsters = Generator.CreateMonsters(6),
            });
        }


        [Test]
        public void CreateADeadlyEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(2440);

            var deadlyDifficulty = _difficulty.Deadly * _party.PartySize;
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp >= deadlyDifficulty);
        }

        [Test]
        public void CreateAHardEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(1440);

            var mediumDifficulty = _difficulty.Medium  * _party.PartySize;
            var deadlyDifficulty = _difficulty.Deadly  * _party.PartySize;
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp >= mediumDifficulty && encounterXp < deadlyDifficulty);
        }

        [Test]
        public void CreateAMediumEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(900);

            var easyDifficulty = _difficulty.Easy  * _party.PartySize;
            var hardDifficulty = _difficulty.Hard  * _party.PartySize;
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp >= easyDifficulty && encounterXp < hardDifficulty);
        }

        [Test]
        public void CreateAnEasyEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(750);

            var mediumDifficulty = _difficulty.Medium  * _party.PartySize;
            var encounterXp = _encounterMock.Object.EncounterExperience;

            Assert.IsTrue(encounterXp < mediumDifficulty);
        }

        [Test]
        public void CreateToEasyOfAnEncounterForAParty()
        {
            _encounterMock.Setup(mock => mock.EncounterExperience).Returns(0);

            var easyDifficulty = _difficulty.Easy  * _party.PartySize;
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

            var encounter = new MonstersController(_dataMock.Object, _encounterMock.Object, _mockMonsterRepository.Object);
            var test = encounter.Encounter().Monsters.ToList();

            Assert.IsTrue(test.Count == 0);
            Assert.IsTrue(test.Any() == false);
        }

        [Test]
        [TestCase(0, 50 * 0)]
        [TestCase(1, 50 * 1 * 1)]
        [TestCase(2, 50 * 2 * 1.5)]
        [TestCase(3, 50 * 3 * 2)]
        [TestCase(4, 50 * 4 * 2)]
        [TestCase(6, 50 * 6 * 2)]
        [TestCase(7, 50 * 7 * 2.5)]
        [TestCase(9, 50 * 9 * 2.5)]
        [TestCase(10, 50 * 10 * 2.5)]
        [TestCase(11, 50 * 11 * 3)]
        [TestCase(13, 50 * 13 * 3)]
        [TestCase(15, 50 * 15 * 4)]
        [TestCase(50, 50 * 50 * 4)]
        public void SizeMonsterMultiplier(int numberOfMonsters, double expectedXpValue)
        {
            const int xp = 50;

            var xpValue = (int)(xp * numberOfMonsters * _encounterService.ApplyMonsterSizeMultiplier(numberOfMonsters));

            Assert.IsTrue(xpValue == (int)expectedXpValue);

        }

        [Test]
        public void EncounterResolverXpRange()
        {

            _mockMonsterRepository.Setup(p => p.GetMonsters(_partyService))
                .Returns(() => new List<MonsterModel>
                {
                    new MonsterModel { Xp = 10 },
                    new MonsterModel { Xp = 1000 }
                });

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateEncounter(_party);
            var encounter = service.MonsterResolver(_partyService);

            Assert.IsTrue(encounter.Count() == 1);
        }

        [Test]
        public void EncounterResolverMonsterIsToBigAndReturnsEmpty()
        {

            _mockMonsterRepository.Setup(p => p.GetMonsters(_partyService))
                .Returns(() => new List<MonsterModel>
                {
                    new MonsterModel { Xp = 9999 }
                });

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateEncounter(_party);
            var encounter = service.MonsterResolver(_partyService);

            Assert.IsFalse(encounter.Any());
        }

        [Test]
        public void AdjustEncounter()
        {
            const int smallXp = 50;
            const int bigXp = 2500;
           List<MonsterViewModel> monsters = new List<MonsterViewModel>
           {
               new MonsterViewModel()
               {
                   ExperienceValue = smallXp,
                   Quantity = 3,
                  Name = "Small monster"
               },
               new MonsterViewModel()
               {
                   ExperienceValue = bigXp,
                   Quantity = 1,
                  Name = "Big monster"
               }
           }; 

            var service = new EncounterService(_mockMonsterRepository.Object);
            var xp = service.GetMonstersExperienceValue(monsters);

            Assert.IsTrue(xp == (smallXp*3 * 2) + bigXp * 1 * 1);

            monsters[1].Quantity++;
            xp = service.GetMonstersExperienceValue(monsters);
            Assert.IsTrue(xp == (smallXp*3*2) + bigXp *2 * 1.5);
        }
    } //Bottom
}