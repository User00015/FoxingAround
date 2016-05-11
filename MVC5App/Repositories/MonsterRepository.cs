using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.ViewModels;

namespace MVC5App.Repositories
{
    public class MonsterRepository : IMonsterRepository
    {
        private const int Single = 1;
        private const int Pair = 2;
        private static readonly IEnumerable<int> Group = Enumerable.Range(3, 3);
        private static readonly IEnumerable<int> Gang = Enumerable.Range(7, 3);
        private static readonly IEnumerable<int> Mob = Enumerable.Range(11, 3);
        private static readonly IEnumerable<int> Horde = Enumerable.Range(15, 100);
        private ITableDataService _tableDataService;

        public MonsterRepository(ITableDataService tableDataService)
        {
            _tableDataService = tableDataService;
        }

        internal void MonsterResolver(EncounterViewModel encounter)
        {
            var monsterList = _tableDataService
        }

        public List<MonsterViewModel> Monsters { get; set; }

        public double GetMonstersSizeMultiplier()
        {
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
            return 0;
        }

        public int GetMonstersExperienceValue()
        {
            return (int)(Monsters.Sum(m => m.ExperienceValue) * GetMonstersSizeMultiplier());
        }
    }
}