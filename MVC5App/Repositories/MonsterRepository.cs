using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Runtime.Internal;
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
            var difficulty = encounter.Party.GetDifficulty();
            var monsterList = _tableDataService.GetAll<MonsterModel>().Where(p => p.Xp <= difficulty).Shuffle();


            //var monsters = shuffledList.TakeWhile(x =>
            //{
            //    sum += x.Xp;
            //    var temp = sum;
            //    return temp < difficulty;
            //});

            var finalList = new List<MonsterViewModel>();
            foreach (var monster in monsterList.TakeWhile(monster => GetMonstersExperienceValue(finalList) < difficulty))
            {
                var experienceRemaining = difficulty - GetMonstersExperienceValue(finalList);

                var amountToAdd = Foo(experienceRemaining);

                var quantity = new Random().Next(0, amountToAdd);

                if (quantity > 0)
                    finalList.Add(new MonsterViewModel
                    {
                        Quantity = quantity,
                        ExperienceValue = monster.Xp * quantity,
                        Level = monster.ChallengeRating,
                        Name = monster.Name

                    });
            Monsters = finalList;
            }


            Monsters = Monsters.OrderByDescending(m => m.ExperienceValue);



            //Monsters = monsterList.Select(monster => new MonsterViewModel()
            //{
            //    ExperienceValue = monster.Xp,
            //    Level = monster.ChallengeRating,
            //    Name = monster.Name
            //}).OrderByDescending(monster => monster.ExperienceValue);
        }

        private int Foo(int experienceRemaining)
        {
            var bar = new List<MonsterViewModel>();

            return experienceRemaining;
        }

        public IEnumerable<MonsterViewModel> Monsters { get; set; } = new List<MonsterViewModel>();

        public double ApplyMonsterSizeMultiplier(int monsters)
        {
            if (monsters == Single)
            {
                return 1;
            }
            if (monsters == Pair)
            {
                return 1.5;
            }
            if (Group.Contains(monsters))
            {
                return 2;
            }
            if (Gang.Contains(monsters))
            {
                return 2.5;
            }
            if (Mob.Contains(monsters))
            {
                return 3;
            }
            if (Horde.Contains(monsters))
            {
                return 4;
            }
            return 0;
        }

        public int GetMonstersExperienceValue(IEnumerable<MonsterViewModel> monsters )
        {
            return (int)monsters.Sum(m => m.ExperienceValue*ApplyMonsterSizeMultiplier(m.Quantity));
        }
    }
}