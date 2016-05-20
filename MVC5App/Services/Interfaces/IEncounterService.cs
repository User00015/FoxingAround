using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services.Interfaces
{
    public interface IEncounterService
    {
        EncounterViewModel Encounter { get; set; }
        void CreateEncounter(IPartyViewModel levels);
        int EncounterExperience { get; }
        void MonsterResolver(IEncounterViewModel encounter);
        double ApplyMonsterSizeMultiplier(int monsters);
        int GetMonstersExperienceValue(IEnumerable<MonsterViewModel> monsters);
    }
}