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
        public IParty Party { get; set; } = new PartyService(new PartyViewModel());
        public int PartyDifficulty => Party.GetDifficulty();
        public int ExperienceValue { get; set; }
    }
}