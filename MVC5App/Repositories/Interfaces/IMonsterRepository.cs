using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.ViewModels;

namespace MVC5App.Repositories.Interfaces
{
    public interface IMonsterRepository
    {
        List<MonsterViewModel> Monsters { get; set; }
        int GetMonstersExperienceValue();
        double GetMonstersSizeMultiplier();
    }
}