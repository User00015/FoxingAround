using System.Collections.Generic;
using MVC5App.Services.Interfaces;

namespace MVC5App.ViewModels.Interfaces
{
    public interface IEncounterViewModel
    {
        IEnumerable<MonsterViewModel> Monsters { get; set; }
        int EncounterExperience { get; set; }
    }
}