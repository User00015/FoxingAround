using System;
using System.Collections.Generic;
using System.Web.Http;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels;

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
            return _monsterRepository.GetMonsters(new PartyService(new PartyViewModel()));
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

        public IHttpActionResult Put(int id, [FromBody]MonsterModel value)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Monsters/ExperienceValues")]
        public int ExperienceValues([FromBody] List<MonsterViewModel> monsterViewModels)
        {
            return _encounterService.GetMonstersExperienceValue(monsterViewModels);
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