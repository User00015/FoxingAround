using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    public class MonsterRepository : IMonsterRepository
    {
        private const int None = 0;
        private const int Single = 1;
        private const int Pair = 2;
        private static readonly IEnumerable<int> Group = Enumerable.Range(3, 3);
        private static readonly IEnumerable<int> Gang = Enumerable.Range(7, 3);
        private static readonly IEnumerable<int> Mob = Enumerable.Range(11, 3);
        private static readonly IEnumerable<int> Horde = Enumerable.Range(15, 100);

        internal void CreateMonsters(PartyViewModel party)
        {
            Monsters = new List<MonsterViewModel>()
            {
                new MonsterViewModel()
                {
                    ExperienceValue = 50,
                    Level = 2,
                    Name = "Foo Monster"
                },
                new MonsterViewModel()
                {
                    ExperienceValue = 60,
                    Level = 2,
                    Name = "Foo Monster"
                },
                new MonsterViewModel()
                {
                    ExperienceValue = 50,
                    Level = 2,
                    Name = "Foo Monster"
                },
                new MonsterViewModel()
                {
                    ExperienceValue = 150,
                    Level = 2,
                    Name = "Foo Monster"
                },
                new MonsterViewModel()
                {
                    ExperienceValue = 50,
                    Level = 2,
                    Name = "Foo Monster"
                },
                new MonsterViewModel()
                {
                    ExperienceValue = 250,
                    Level = 3,
                    Name = "Foo Monster"
                },
            };
        
        }

        public List<MonsterViewModel> Monsters { get; set; }

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

        public int ExperienceValue()
        {
            return (int)(Monsters.Sum(m => m.ExperienceValue) * GetMonsterSizeMultiplier());
        }
    }
}