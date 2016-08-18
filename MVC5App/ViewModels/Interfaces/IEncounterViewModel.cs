using System.Collections.Generic;
using MVC5App.Services.Interfaces;

namespace MVC5App.ViewModels.Interfaces
{
    public interface IEncounterViewModel
    {
        List<MonsterViewModel> Monsters { get; set; }
        int EncounterExperience { get; set; }
        DifficultyViewModel Difficulty { get; set; }
    }
}