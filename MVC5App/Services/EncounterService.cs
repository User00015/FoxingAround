using System.Collections.Generic;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    public class EncounterService : IEncounterService
    {
        public EncounterViewModel Encounter { get; set; }

        private readonly MonsterRepository _monsterRepository = new MonsterRepository();

        public int GetEncounterExperience()
        {
            return _monsterRepository.ExperienceValue();
        }
        public void CreateEncounter(List<int> levels )
        {
            Encounter = new EncounterViewModel
            {
                Party = new PartyViewModel(levels)
            };

            _monsterRepository.CreateMonsters(Encounter.Party);
            Encounter.Monsters = _monsterRepository.Monsters;
        }
    }
}