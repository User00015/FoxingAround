using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Repositories.Interfaces
{
    public interface IMonsterRepository
    {
        IEnumerable<MonsterViewModel> Monsters { get; set; }
        int GetMonstersExperienceValue(IEnumerable<MonsterViewModel> monsters );
        double ApplyMonsterSizeMultiplier(int monsters);
        void MonsterResolver(IEncounterViewModel encounter);
    }
}