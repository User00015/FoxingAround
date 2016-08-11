using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Environment = MVC5App.Models.Environment;

namespace MVC5App.Controllers
{
    public class MonstersController : ApiController
    {
        private readonly ITableDataService _tableDataService;
        private readonly IEncounterService _encounterService;
        private readonly IMonsterRepository _monsterRepository;

        public MonstersController(ITableDataService tableDataService, IEncounterService encounterService, IMonsterRepository monsterRepository)
        {
            _tableDataService = tableDataService;
            _encounterService = encounterService;
            _monsterRepository = monsterRepository;
        }

        [HttpGet]
        public IEnumerable<MonsterModel> Get()
        {
            return _monsterRepository.GetMonsters();
        }

        [HttpGet]
        public MonsterModel Get(int id)
        {
            return _monsterRepository.GetMonster(id);
        }

        [HttpPost]
        public EncounterViewModel Post([FromBody] PartyViewModel party)
        {
            _encounterService.CreateEncounter(party);
            return _encounterService.Encounter;
        }

        [Authorize]
        [HttpGet]
        public EncounterViewModel SavedEncounter()
        {
            _encounterService.Encounter = new EncounterViewModel()
            {
                Difficulty = new DifficultyViewModel()
                {
                    Deadly = 1000,
                    Hard = 750,
                    Medium = 500,
                    Easy = 250,
                    ExperienceValue = 650
                },
                EncounterExperience = 650,
                Monsters = new List<MonsterViewModel>()
                {
                    new MonsterViewModel()
                    {
                        ExperienceValue = 500,
                        Quantity = 1,
                        Name = "saved encounter boss",
                        Id = 1,
                        Level = "3"
                    },
                    new MonsterViewModel()
                    {
                        ExperienceValue = 50,
                        Quantity = 3,
                        Name = "saved encounter minion",
                        Id = 2,
                        Level = "1"
                    }
                }
            };
            return _encounterService.Encounter;
        }


        [HttpPost]
        public int ExperienceValues([FromBody] List<MonsterViewModel> monsterViewModels)
        {
            return _encounterService.GetEncountersExperienceValue(monsterViewModels);
        }

        public IHttpActionResult Delete(object item)
        {
            throw new NotImplementedException();
        }

        public EncounterViewModel Encounter()
        {
            return _encounterService.Encounter;
        }

       
    }
}