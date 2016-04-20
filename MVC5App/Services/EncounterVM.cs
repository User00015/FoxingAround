using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    // ReSharper disable once InconsistentNaming
    public class EncounterVM : IEncounterVM
    {
        private const int None = 0;
        private const int Single = 1;
        private const int Pair = 2;
        private static readonly IEnumerable<int> Group = Enumerable.Range(3, 3);
        private static readonly IEnumerable<int> Gang = Enumerable.Range(7, 3);
        private static readonly IEnumerable<int> Mob = Enumerable.Range(11, 3);
        private static readonly IEnumerable<int> Horde = Enumerable.Range(15, 100);

        public List<MonsterViewModel> Monsters { get; set; }

        public int ExperienceValue()
        {
            return (int)(Monsters.Sum(m => m.ExperienceValue) * GetMonsterSizeMultiplier());
        }

        public double GetMonsterSizeMultiplier()
        {

            if (Monsters.Count == None)
            {
                return 0;
            }
            if (Monsters.Count == Single)
            {
                return 1;
            }
            if (Monsters.Count == Pair)
            {
                return 1.5;
            }
            if (Group.Contains(Monsters.Count))
            {
                return 2;
            }
            if (Gang.Contains(Monsters.Count))
            {
                return 2.5;
            }
            if (Mob.Contains(Monsters.Count))
            {
                return 3;
            }
            if (Horde.Contains(Monsters.Count))
            {
                return 4;
            }
            return 4;
        }

        public PartyViewModel Party { get; set; }
    }
}