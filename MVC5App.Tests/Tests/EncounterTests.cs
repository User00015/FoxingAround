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
        private IPartyViewModel _party;
        private EncounterService _encounterService;
        private Difficulty _difficulty;
        private PartyService _partyService;


        [SetUp]
        public void Init()
        {
            _encounterMock = new Mock<IEncounterService>();
            _dataMock = new Mock<ITableDataService>();
            _mockMonsterRepository = new Mock<IMonsterRepository>();
            _encounterService = new EncounterService(_mockMonsterRepository.Object);
            _party = new PartyViewModel
            {
                PartyLevel = 3,
                PartySize = 6,
                Difficulty = 3,
                Environment = -1
            };

            /* Party difficulties:
             * Easy: 450
             * Medium: 900
             * Hard: 1350
             * Deadly: 2400
             */

            _partyService = new PartyService(_party);

            _difficulty = new Difficulty(_party.PartyLevel);
            _encounterService.Encounter = new EncounterViewModel();
        }


        [Test]
        public void CreateADeadlyEncounterForAParty()
        {
            _encounterService.Encounter.Monsters = Generator.CreateMonsters(15); //50 * 15 * 4 = 3000
            var deadlyDifficulty = _difficulty.Deadly * _party.PartySize;
            var encounterXp = _encounterService.GetEncountersExperienceValue(_encounterService.Encounter.Monsters);

            Assert.IsTrue(encounterXp >= deadlyDifficulty);
        }

        [Test]
        public void CreateAHardEncounterForAParty()
        {

            _encounterService.Encounter.Monsters = Generator.CreateMonsters(8); // 50 * 8 * 3 = 1200
            var mediumDifficulty = _difficulty.Medium * _party.PartySize;
            var deadlyDifficulty = _difficulty.Deadly * _party.PartySize;
            var encounterXp = _encounterService.GetEncountersExperienceValue(_encounterService.Encounter.Monsters);

            Assert.IsTrue(encounterXp >= mediumDifficulty && encounterXp < deadlyDifficulty);
        }

        [Test]
        public void CreateAMediumEncounterForAParty()
        {

            _encounterService.Encounter.Monsters = Generator.CreateMonsters(6); // 50 * 6 * 2 = 600
            var easyDifficulty = _difficulty.Easy * _party.PartySize;
            var hardDifficulty = _difficulty.Hard * _party.PartySize;
            var encounterXp = _encounterService.GetEncountersExperienceValue(_encounterService.Encounter.Monsters);

            Assert.IsTrue(encounterXp >= easyDifficulty && encounterXp < hardDifficulty);
        }

        [Test]
        public void CreateAnEasyEncounterForAParty()
        {

            _encounterService.Encounter.Monsters = Generator.CreateMonsters(5); // 50 * 5 * 2 = 500
            var mediumDifficulty = _difficulty.Medium * _party.PartySize;
            var easyDifficulty = _difficulty.Easy * _party.PartySize;
            var encounterXp = _encounterService.GetEncountersExperienceValue(_encounterService.Encounter.Monsters);

            Assert.IsTrue(encounterXp < mediumDifficulty && encounterXp > easyDifficulty);
        }

        [Test]
        public void CreateToEasyOfAnEncounterForAParty()
        {

            _encounterService.Encounter.Monsters = Generator.CreateMonsters(1); // 50 * 1 * 1 = 50
            var easyDifficulty = _difficulty.Easy * _party.PartySize;
            var encounterXp = _encounterService.GetEncountersExperienceValue(_encounterService.Encounter.Monsters);
            //var encounterXp = _encounterService.EncounterExperience;


            Assert.IsTrue(encounterXp < easyDifficulty && encounterXp > 0);
        }

        [Test]
        public void CreateEmptyEncounterForAParty()
        {
            _encounterService.Encounter.Monsters = new List<MonsterViewModel>();
            var encounterXp = _encounterService.GetEncountersExperienceValue(_encounterService.Encounter.Monsters);
            Assert.IsTrue(encounterXp == 0);
        }

        [Test]
        public void GetMonsterViewModel()
        {
            var mockMonster = Generator.CreateMonsters(0);
            _encounterMock.Setup(mock => mock.Encounter).Returns(new EncounterViewModel
            {
                Monsters = mockMonster
            });

            var encounter = new MonstersController(_encounterMock.Object, _mockMonsterRepository.Object);
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
                    new MonsterModel { Xp = 10, Id = 1 },
                    new MonsterModel { Xp = 1000, Id = 2 }
                });

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateRandomEncounter(_party);
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
            service.CreateRandomEncounter(_party);
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
                   Quantity = 1,
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
            var xp = service.GetEncountersExperienceValue(monsters);

            Assert.IsTrue(xp == (smallXp + bigXp) * 1.5);

            monsters[1].Quantity++;
            xp = service.GetEncountersExperienceValue(monsters);
            Assert.IsTrue(xp == (smallXp + bigXp * 2) * 2);
        }

        [Test]
        public void AddOneMonsterToEncounter()
        {
            List<MonsterViewModel> monsters = new List<MonsterViewModel>
           {
               new MonsterViewModel()
               {
                   ExperienceValue = 50,
                   Quantity = 1,
                   Id = 5,
                  Name = "Small monster"
               },
               new MonsterViewModel()
               {
                   ExperienceValue = 500,
                   Quantity = 1,
                   Id = 10,
                  Name = "Big monster"
               }
           };

            var monster = new MonsterModel()
            {
                Xp = 500,
                Id = 1,
                Name = "added monster"
            };

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateRandomEncounter(_party);
            service.RandomNumber = new MockRandomNumber();
            var quantityAdded = service.CalculateQuantityToAdd(monsters,monster);

            Assert.IsTrue(quantityAdded == 1);
        }
        [Test]
        public void AddFourMonstersToEncounter()
        {
            List<MonsterViewModel> monsters = new List<MonsterViewModel>
           {
               new MonsterViewModel()
               {
                   ExperienceValue = 50,
                   Quantity = 1,
                   Id = 5,
                  Name = "Small monster"
               },
               new MonsterViewModel()
               {
                   ExperienceValue = 500,
                   Quantity = 1,
                   Id = 10,
                  Name = "Big monster"
               }
           };

            var monster = new MonsterModel()
            {
                Xp = 200,
                Id = 1,
                Name = "Medium monster"
            };

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateRandomEncounter(_party);
            service.RandomNumber = new MockRandomNumber();
            var quantityAdded = service.CalculateQuantityToAdd(monsters, monster);

            Assert.IsTrue(quantityAdded  == 3);
        }

        [Test]
        public void AddTwoDifferentMonstersToEncounter()
        {
            List<MonsterViewModel> monsters = new List<MonsterViewModel>
           {
               new MonsterViewModel()
               {
                   ExperienceValue = 50,
                   Quantity = 1,
                   Id = 5,
                  Name = "Small monster"
               },
               new MonsterViewModel()
               {
                   ExperienceValue = 500,
                   Quantity = 1,
                   Id = 10,
                  Name = "Big monster"
               }
           };

            var monster1 = new MonsterModel()
            {
                Id = 1,
                Xp = 200,
                Name = "Medium monster 1"
            };


            var monster2 = new MonsterModel()
            {
                Id = 2,
                Xp = 200,
                Name = "Medium monster 2"
            };

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateRandomEncounter(_party);
            service.RandomNumber = new MockRandomNumber();
            var quantityMonster1 = service.CalculateQuantityToAdd(monsters, monster1);

            monsters.Add(new MonsterViewModel()
            {
                Id = 1,
                ExperienceValue = 200,
                Name = "Added monster",
                Quantity = 2
            });


            service.RandomNumber = new MockRandomNumber();
            var quantityMonster2 = service.CalculateQuantityToAdd(monsters, monster2);


            Assert.IsTrue(quantityMonster1 == 3);
            Assert.IsTrue(quantityMonster2 == 1);
        }

        [Test]
        public void CalculateQuantityToAddShouldNeverReturnLessThanZero()
        {

            List<MonsterViewModel> monsters = new List<MonsterViewModel>();

            var service = new EncounterService(_mockMonsterRepository.Object);
            service.CreateRandomEncounter(_party);
            service.RandomNumber = new MockRandomNumber();

            var monster = new MonsterModel()
            {
                Xp = 9999,
                Id = 1,
                Name = "Too big"
            };

            var quantityAdded = service.CalculateQuantityToAdd(monsters, monster);


            Assert.IsTrue(quantityAdded == 0);
        }

    } //Bottom of tests
}