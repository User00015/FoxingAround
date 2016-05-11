using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services.Models
{
    public class Party : IParty
    {
        private static List<int> _levels = new List<int>();


        public Party(IPartyViewModel party) 
        {
            _levels = AddDifficultyLevels(party);

            foreach (var level in _levels)
            {
                Difficulties.Add(new Difficulty(level));

            }
        }

        private List<Difficulty> Difficulties { get; } = new List<Difficulty>();

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

        private List<int> AddDifficultyLevels(IPartyViewModel party) //TODO - Later on a level will be associated with a character instead of this simple calculation.
        {
            _levels.Clear();
            for (var i = 0; i < party.PartySize; ++i)
            {
                _levels.Add(party.PartyLevel);
            }
            return _levels;
        }
    }
}