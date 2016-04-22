using System.Collections.Generic;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    public interface IMonsterRepository
    {
        List<MonsterViewModel> Monsters { get; set; }
        int ExperienceValue();
        double GetMonsterSizeMultiplier();
    }
}