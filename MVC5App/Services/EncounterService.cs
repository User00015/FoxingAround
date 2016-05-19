using System;
using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Models;
using MVC5App.Repositories;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services.Interfaces;
using MVC5App.Services.Models;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services
{
    public class EncounterService : IEncounterService
    {
        private const int Single = 1;
        private const int Pair = 2;
        private static readonly IEnumerable<int> Group = Enumerable.Range(3, 3);
        private static readonly IEnumerable<int> Gang = Enumerable.Range(7, 3);
        private static readonly IEnumerable<int> Mob = Enumerable.Range(11, 3);
        private static readonly IEnumerable<int> Horde = Enumerable.Range(15, 100);

        public EncounterViewModel Encounter { get; set; }

        private readonly IMonsterRepository _monsterRepository;

        public EncounterService(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        public int EncounterExperience => GetMonstersExperienceValue(Encounter.Monsters);

        public void CreateEncounter(IPartyViewModel party)
        {
            Encounter = new EncounterViewModel
            {
                Party = new Party(party),
            };

            MonsterResolver(Encounter);
            Encounter.ExperienceValue = GetMonstersExperienceValue(Encounter.Monsters);
        }

        public void MonsterResolver(IEncounterViewModel encounter)
        {
            var difficulty = encounter.Party.GetDifficulty();
            var monsterList = _monsterRepository.GetMonsters(encounter);

            var finalList = new List<MonsterViewModel>();
            foreach (var monster in monsterList)
            {

                var experienceRemaining = difficulty - GetMonstersExperienceValue(finalList);
                var amountToAdd = CalculateQuantityToAdd(experienceRemaining, monster);

                var quantity = new Random().Next(amountToAdd / 2, amountToAdd);

                if (quantity > 0)
                    finalList.Add(new MonsterViewModel
                    {
                        Quantity = quantity,
                        ExperienceValue = monster.Xp * quantity,
                        Level = monster.ChallengeRating,
                        Name = monster.Name

                    });
                Encounter.Monsters = finalList;

                var previousDifficulty = Encounter.Party.GetDifficulty(Encounter.Party.Difficulty - 1);

                if (previousDifficulty > 0 && GetMonstersExperienceValue(finalList) > previousDifficulty)
                    break;


            }

            Encounter.Monsters = Encounter.Monsters.OrderByDescending(m => m.ExperienceValue);
        }

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


        public int GetMonstersExperienceValue(IEnumerable<MonsterViewModel> monsters)
        {
            return (int)monsters.Sum(m => m.ExperienceValue * ApplyMonsterSizeMultiplier(m.Quantity));
        }

        private int CalculateQuantityToAdd(int experienceRemaining, MonsterModel monster)
        {
            var quantity = 0;
            while (quantity * monster.Xp * ApplyMonsterSizeMultiplier(quantity) < experienceRemaining)
            {
                quantity += 1;
            }

            return quantity;
        }
    }
}