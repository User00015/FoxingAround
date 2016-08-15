using System.Collections.Generic;
using MVC5App.Models;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Repositories.Interfaces
{
    public interface IMonsterRepository
    {
        IEnumerable<MonsterModel> GetMonsters(IPartyService encounter);
        MonsterModel GetMonster(int id);
        IEnumerable<MonsterModel> GetMonsters();
        IEnumerable<EncounterViewModel> GetSavedEncounters(SavedEncountersViewModel model);
    }
}