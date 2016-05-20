using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.Services;
using MVC5App.Services.Interfaces;
using MVC5App.Services.Models;

namespace MVC5App.ViewModels.Interfaces
{
    public interface IEncounterViewModel
    {
        IEnumerable<MonsterViewModel> Monsters { get; set; }
        IParty Party { get; set; }
        int GetPartyDifficulty { get; }
        int ExperienceValue { get; set; }
    }
}