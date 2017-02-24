using System;
using System.Collections.Generic;
using System.Media;
using MVC5App.Models;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services.Interfaces
{
    public interface IEncounterService
    {
        EncounterViewModel Encounter { get; set; }
        void CreateRandomEncounter(IPartyViewModel levels);
        IEnumerable<MonsterViewModel> MonsterResolver(IPartyService party);
        double ApplyMonsterSizeMultiplier(int monsters);
        int GetEncountersExperienceValue(IEnumerable<MonsterViewModel> monsters);
        int CalculateQuantityToAdd(IList<MonsterViewModel> finalList, IMonsterModel monster);
        void CreateEmptyEncounter(IPartyViewModel levels);


    }
}