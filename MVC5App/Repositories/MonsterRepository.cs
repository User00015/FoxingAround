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
        private readonly IEnumerable<MonsterModel> _allMonsters;

        public MonsterRepository(ITableDataService tableDataService)
        {
            _tableDataService = tableDataService;
            _allMonsters = _tableDataService.GetAll<MonsterModel>();
        }

        public IEnumerable<MonsterModel> GetMonsters(IEncounterViewModel encounter)
        {
            var difficulty = encounter.GetPartyDifficulty;
            return _allMonsters.Where(p => p.Xp <= difficulty);
        }

        public MonsterModel GetMonster(int id)
        {
            return _allMonsters.SingleOrDefault(monster => monster.Id == id);
        }
    }
}