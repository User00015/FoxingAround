using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVC5App.Services
{
    public class PartyViewModel : IPartyViewModel
    {
        public PartyViewModel(List<int> levels)
        {
            Difficulties = new List<Difficulty>();

            foreach (var level in levels)
            {
                Difficulties.Add(new Difficulty(level));
                
            }
        }

        private List<Difficulty> Difficulties { get; set; }

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

    }
}