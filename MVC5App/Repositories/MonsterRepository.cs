using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Runtime.Internal;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Extensions;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Repositories
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly ITableDataService _tableDataService;

        public MonsterRepository(ITableDataService tableDataService)
        {
            _tableDataService = tableDataService;
        }

        public IEnumerable<MonsterModel> GetMonsters(IEncounterViewModel encounter)
        {
            var difficulty = encounter.Party.GetDifficulty();
            return _tableDataService.GetAll<MonsterModel>().Where(p => p.Xp <= difficulty).Shuffle();
        }

    }
}