using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.Models;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Repositories.Interfaces
{
    public interface IMonsterRepository
    {
        IEnumerable<MonsterModel> GetMonsters(IEncounterViewModel encounter);
    }
}