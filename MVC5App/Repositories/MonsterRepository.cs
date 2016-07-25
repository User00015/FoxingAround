using System.Collections.Generic;
using System.Linq;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Repositories
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly IEnumerable<MonsterModel> _allMonsters;

        public MonsterRepository(ITableDataService tableDataService)
        {
            _allMonsters = tableDataService.GetAll<MonsterModel>().ToList();
        }

        public IEnumerable<MonsterModel> GetMonsters(IPartyService encounter)
        {
            var xp = encounter.GetDifficulty();
            return _allMonsters.Where(p => p.Xp <= xp);
        }

        public MonsterModel GetMonster(int id)
        {
            return _allMonsters.SingleOrDefault(monster => monster.Id == id);
        }
    }
}