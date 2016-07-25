using System.Collections.Generic;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.ViewModels
{
    // ReSharper disable once InconsistentNaming
    public class EncounterViewModel : IEncounterViewModel
    {
        public IEnumerable<MonsterViewModel> Monsters { get; set; }  = new List<MonsterViewModel>();
        public IPartyViewModel PartyViewModel { get; set; }
        public IDifficultyViewModel Difficulty { get; set; } = new DifficultyViewModel();
    }

    public class DifficultyViewModel : IDifficultyViewModel
    {
        public int Easy { get; set; }
        public int Medium { get; set; }
        public int Hard { get; set; }
        public int Deadly { get; set; }
        public int ExperienceValue { get; set; }
    }
}