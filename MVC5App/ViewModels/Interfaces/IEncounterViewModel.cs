using System.Collections.Generic;
using MVC5App.Services.Interfaces;

namespace MVC5App.ViewModels.Interfaces
{
    public interface IEncounterViewModel
    {
        IEnumerable<MonsterViewModel> Monsters { get; set; }
        IPartyViewModel PartyViewModel { get; set; }
        IDifficultyViewModel Difficulty { get; set; }
        
    }
}