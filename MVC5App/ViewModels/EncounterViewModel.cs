using System.Collections.Generic;
using Amazon.Util.Internal.PlatformServices;
using MVC5App.Models;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.ViewModels
{
    // ReSharper disable once InconsistentNaming
    public class EncounterViewModel : IEncounterViewModel
    {
        public IEnumerable<MonsterViewModel> Monsters { get; set; }  = new List<MonsterViewModel>();
        public int EncounterExperience { get; set;  }
        public IDifficultyViewModel Difficulty { get; set; } = new DifficultyViewModel();
    }
}