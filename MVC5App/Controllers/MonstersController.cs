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
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels;
using Newtonsoft.Json;

namespace MVC5App.Controllers
{
    public class MonstersController : ApiController
    {
        private readonly ITableDataService _tableDataService;
        private readonly IEncounterService _encounterService;

        public MonstersController(ITableDataService tableDataService, IEncounterService encounterService)
        {
            _tableDataService = tableDataService;
            _encounterService = encounterService;
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<MonsterModel> Get()
        {
            return _tableDataService.GetAll<MonsterModel>();
        }

        // GET api/<controller>/5
        [HttpGet]
        public MonsterModel Get(int id)
        {
            return _tableDataService.GetItem<MonsterModel>(id);
        }

        //POST api/<controller>
        [HttpPost]
        public EncounterViewModel Post([FromBody] PartyViewModel party)
        {
            _encounterService.CreateEncounter(party);
            return _encounterService.Encounter;
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]MonsterModel value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
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