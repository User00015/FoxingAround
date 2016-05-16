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
        private int _difficulty;

        public Party(IPartyViewModel party)
        {
            _levels = AddDifficultyLevels(party);
            _difficulty = party.Difficulty;

            foreach (var level in _levels)
            {
                Difficulties.Add(new Difficulty(level));
            }
        }

        private List<Difficulty> Difficulties { get; } = new List<Difficulty>();

        public int GetDifficulty()
        {
            switch (_difficulty)
            {
                case 0:
                    return Difficulties.Sum(m => m.Easy);
                case 1:
                    return Difficulties.Sum(m => m.Medium);
                case 2:
                    return Difficulties.Sum(m => m.Hard);
                case 3:
                    return Difficulties.Sum(m => m.Deadly);
                default:
                    return 0;
            }
        }

        public int GetDifficulty(int difficultyLevel)
        {
            _difficulty = difficultyLevel;
            return GetDifficulty();
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