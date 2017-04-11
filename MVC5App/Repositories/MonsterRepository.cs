using System.Collections.Generic;
using System.Linq;
using System.Web.DynamicData;
using Amazon.DynamoDBv2;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Repositories
{
    public class MonsterRepository : IMonsterRepository
    {
        private readonly IEnumerable<MonsterModel> _allMonsters;
        private readonly ITableDataService _tableDataService;

        public MonsterRepository(ITableDataService tableDataService)
        {
            _tableDataService = tableDataService;
            _allMonsters = _tableDataService.GetAll<MonsterModel>().ToList();
        }

        public IEnumerable<MonsterModel> GetMonsters(IPartyService encounter)
        {
            var xp = encounter.CurrentDifficulty();
            return _allMonsters.Where(p => p.Xp <= xp);
        }

        public MonsterModel GetMonster(int id)
        {
            return _allMonsters.SingleOrDefault(monster => monster.Id == id);
        }

        public IEnumerable<MonsterModel> GetMonsters()
        {
            return _allMonsters;
        }

        public IEnumerable<EncounterViewModel> GetSavedEncounters(string email)
        {
            return _tableDataService.GetItem<SavedEncountersViewModel>(email)?.Encounters;
        }

        public void SaveEncounters(SavedEncountersViewModel model)
        {
            _tableDataService.Store(model);
        }
    }
}