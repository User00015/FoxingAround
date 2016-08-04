using System.Collections.Generic;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services.Interfaces
{
    public interface IEncounterService
    {
        EncounterViewModel Encounter { get; set; }
        void CreateEncounter(IPartyViewModel levels);
        int EncounterExperience { get; }
        IEnumerable<MonsterViewModel> MonsterResolver(IPartyService party);
        double ApplyMonsterSizeMultiplier(int monsters);
        int GetEncountersExperienceValue(IEnumerable<MonsterViewModel> monsters);
    }
}