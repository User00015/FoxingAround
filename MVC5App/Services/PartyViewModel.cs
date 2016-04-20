using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVC5App.Services
{
    public class PartyViewModel : IPartyViewModel
    {
        private List<Difficulty> _difficulties { get; set; }
        public PartyViewModel(List<int> levels)
        {
            _difficulties = new List<Difficulty>();
            Levels = levels;
            SetPartyDifficulty();
        }

        private List<int> Levels { get; set; }

        public List<Difficulty> Difficulty => _difficulties;


        private void SetPartyDifficulty()
        {
            foreach (var level in Levels)
            {
                _difficulties.Add(new Difficulty(level));

            }
        }
    }
}