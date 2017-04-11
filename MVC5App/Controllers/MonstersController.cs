using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Results;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories;
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
        private readonly IEncounterService _encounterService;
        private readonly IMonsterRepository _monsterRepository;

        public MonstersController(IEncounterService encounterService, IMonsterRepository monsterRepository)
        {
            _encounterService = encounterService;
            _monsterRepository = monsterRepository;
        }

        //[HttpGet]
        //public IEnumerable<MonsterModel> Get()
        //{
        //    return _monsterRepository.GetMonsters();
        //}

        [HttpGet]
        public IEnumerable<MonsterViewModel> Get()
        {
            var monsters = _monsterRepository.GetMonsters();
            return monsters.Select(p => new MonsterViewModel()
            {
                Name = p.Name,
                ExperienceValue = p.Xp,
                Id = p.Id,
                Level = p.ChallengeRating,
                Quantity = 1,
                Environment = p.Environment

            }).OrderBy(p => p.Name);
        }

        [HttpGet]
        public MonsterModel Get(int id)
        {
            return _monsterRepository.GetMonster(id);
        }

        [HttpPost]
        public EncounterViewModel Post([FromBody] PartyViewModel party)
        {
            _encounterService.CreateRandomEncounter(party);
            return _encounterService.Encounter;
        }

        [HttpPost]
        public EncounterViewModel Empty([FromBody] PartyViewModel party)
        {
            _encounterService.CreateEmptyEncounter(party);
            return _encounterService.Encounter;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<EncounterViewModel> LoadEncounters()
        {
            var identity = User.Identity as ClaimsIdentity;
            var email = identity?.Claims.SingleOrDefault(p => p.Type == "name")?.Value;
            return _monsterRepository.GetSavedEncounters(email);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult SaveEncounters([FromBody] SavedEncountersViewModel model)
        {
            var identity = User.Identity as ClaimsIdentity;
            model.Email = identity?.Claims.SingleOrDefault(p => p.Type == "name")?.Value;
            _monsterRepository.SaveEncounters(model);

            return Ok();
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