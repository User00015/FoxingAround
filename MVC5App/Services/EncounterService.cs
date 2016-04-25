using System.Collections.Generic;
using System.Linq;
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
        public void CreateEncounter(IPartyViewModel party)
        {
            Encounter = new EncounterViewModel
            {
                Party = new PartyDifficulties(party)
            };

            _monsterRepository.CreateMonsters(Encounter);
            Encounter.Monsters = _monsterRepository.Monsters;
        }
    }
}