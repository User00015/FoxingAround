using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVC5App.DynamoDb;
using MVC5App.Services;

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
        public IEnumerable<StudentVM> Get()
        {
            return _tableDataService.GetAll<StudentVM>();
        }

        // GET api/<controller>/5
        public StudentVM Get(int id)
        {
            return _tableDataService.GetItem<StudentVM>(id);
        }

        // POST api/<controller>
        public void Post()
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EncounterVM Encounter()
        {
            return _encounterService.Encounter;
        }
    }
}