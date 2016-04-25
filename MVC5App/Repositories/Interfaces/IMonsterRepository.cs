using System.Collections.Generic;
using MVC5App.Controllers;

namespace MVC5App.Repositories.Interfaces
{
    public interface IMonsterRepository
    {
        List<MonsterViewModel> Monsters { get; set; }
        int ExperienceValue();
        double GetMonsterSizeMultiplier();
    }
}