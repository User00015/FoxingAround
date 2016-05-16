using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Repositories;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services.Interfaces;
using MVC5App.Services.Models;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services
{
    public class EncounterService : IEncounterService
    {
        public EncounterViewModel Encounter { get; set; }

        private readonly IMonsterRepository _monsterRepository;

        public EncounterService(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        public int EncounterExperience => _monsterRepository.GetMonstersExperienceValue();

        public void CreateEncounter(IPartyViewModel party)
        {
            Encounter = new EncounterViewModel
            {
                Party = new Party(party)
            };

            _monsterRepository.MonsterResolver(Encounter);
            Encounter.Monsters = _monsterRepository.Monsters;
        }
    }
}