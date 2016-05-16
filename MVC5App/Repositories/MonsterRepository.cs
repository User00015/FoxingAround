using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Extensions;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

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
        private readonly ITableDataService _tableDataService;

        public MonsterRepository(ITableDataService tableDataService)
        {
            _tableDataService = tableDataService;
        }

        public void MonsterResolver(IEncounterViewModel encounter)
        {
            var monsters = _tableDataService.GetAll<MonsterModel>();
            var difficulty = encounter.Party.GetDifficulty();

            var sum = 0;
            var monsterList = monsters.Where(p => p.Xp <= difficulty).Shuffle().TakeWhile(x =>
            {
                sum += x.Xp;
                var temp = sum;
                return temp < difficulty;
            });


            Monsters = monsterList.Select(monster => new MonsterViewModel()
            {
                ExperienceValue = monster.Xp,
                Level = monster.ChallengeRating,
                Name = monster.Name
            }).OrderByDescending(monster => monster.ExperienceValue);
        }

        public IEnumerable<MonsterViewModel> Monsters { get; set; }

        public double GetMonstersSizeMultiplier()
        {
            var monsters = Monsters.ToList();
            if (monsters.Count == Single)
            {
                return 1;
            }
            if (monsters.Count == Pair)
            {
                return 1.5;
            }
            if (Group.Contains(monsters.Count))
            {
                return 2;
            }
            if (Gang.Contains(monsters.Count))
            {
                return 2.5;
            }
            if (Mob.Contains(monsters.Count))
            {
                return 3;
            }
            if (Horde.Contains(monsters.Count))
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