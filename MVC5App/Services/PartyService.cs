using System.Collections.Generic;
using System.Linq;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services
{
    public class PartyService : IPartyService
    {
        private static List<int> _levels = new List<int>();

        public PartyService(IPartyViewModel party)
        {
            _levels = AddDifficultyLevels(party);
            Difficulty = party.Difficulty;

            foreach (var level in _levels)
            {
                Difficulties.Add(new Difficulty(level));
            }
        }

        private IList<Difficulty> Difficulties { get; } = new List<Difficulty>();

        public int TotalDeadlyXP => Difficulties.Sum(p => p.Deadly);
        public int TotalHardXP => Difficulties.Sum(p => p.Hard);
        public int TotalMediumXP => Difficulties.Sum(p => p.Medium);
        public int TotalEasyXP => Difficulties.Sum(p => p.Easy);

        public int GetDifficulty()
        {
            return GetDifficulty(Difficulty);
        }

        public int Difficulty { get; }

        public int GetCurrentDifficulty()
        {
            return GetDifficulty(Difficulty);
        }

        public int GetDifficulty(int difficultyLevel)
        {
            switch (difficultyLevel)
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