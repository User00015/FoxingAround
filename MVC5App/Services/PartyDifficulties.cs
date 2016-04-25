using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    public class PartyDifficulties : IPartyDifficulties
    {
        private static List<int> levels = new List<int>();


        public PartyDifficulties(IPartyViewModel party) 
        {
            levels = AddDifficultyLevels(party);

            foreach (var level in levels)
            {
                Difficulties.Add(new Difficulty(level));

            }
        }

        private List<Difficulty> Difficulties { get; set; } = new List<Difficulty>();

        public int TotalDeadlyXP
        {
            get { return Difficulties.Sum(m => m.Deadly); }
        }

        public int TotalHardXP
        {
            get { return Difficulties.Sum(m => m.Hard); }
        }

        public int TotalMediumXP
        {
            get { return Difficulties.Sum(m => m.Medium); }
        }

        public int TotalEasyXP
        {
            get { return Difficulties.Sum(m => m.Easy); }
        }

        private List<int> AddDifficultyLevels(IPartyViewModel party)
        {
            levels.Clear();
            for (var i = 0; i < party.PartySize; ++i)
            {
                levels.Add(party.PartyLevel);
            }
            return levels;
        }
    }
}