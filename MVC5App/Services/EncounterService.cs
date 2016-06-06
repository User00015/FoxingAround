using System;
using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;
using MVC5App.DynamoDb;
using MVC5App.Extensions;
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
        private static readonly IEnumerable<int> Group = Enumerable.Range(3, 4);
        private static readonly IEnumerable<int> Gang = Enumerable.Range(7, 4);
        private static readonly IEnumerable<int> Mob = Enumerable.Range(11, 4);
        private static readonly IEnumerable<int> Horde = Enumerable.Range(15, 100);

        public EncounterViewModel Encounter { get; set; }

        private readonly IMonsterRepository _monsterRepository;
        public int MaximumAmountPerMonster { get; set; } = 5;
        public double MinimumExperienceThreshold { get; set; } = .25;
        public double VariableEncounterExperienceThreshold { get; set; } = .9;

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

            Encounter.Monsters = MonsterResolver(Encounter).OrderByDescending(p => p.ExperienceValue);
            Encounter.ExperienceValue = GetMonstersExperienceValue(Encounter.Monsters);
        }

        public IEnumerable<MonsterViewModel> MonsterResolver(IEncounterViewModel encounter)
        {
            var finalList = new List<MonsterViewModel>();

            foreach (var monster in _monsterRepository.GetMonsters(encounter).Shuffle())
            {
                //Find the maximum amount of a monster that can be added to an encounter
                var quantity = CalculateQuantityToAdd(finalList, monster);

                if (quantity  > 0)
                    finalList.Add(new MonsterViewModel
                    {
                        Quantity = quantity ,
                        ExperienceValue = monster.Xp * quantity,
                        Level = monster.ChallengeRating,
                        Name = monster.Name,
                        Id = monster.Id
                    });

                var threshold = XPThreshold(finalList);
                finalList.RemoveAll(m => m.ExperienceValue < threshold);

                //If the total encounter experience is close to the target difficulty, stop. Prevents 'stuffing' an encounter with increasingly smaller/fewer enemies.
                if (GetMonstersExperienceValue(finalList) > Encounter.GetPartyDifficulty * VariableEncounterExperienceThreshold)
                    break;
            }

            return finalList;
        }

        private int XPThreshold(List<MonsterViewModel> finalList)
        {
            var threshold = 0;
            if (finalList.Any())
            {
                threshold = (int)(finalList.Max(m => m.ExperienceValue) * MinimumExperienceThreshold);
            }
            return threshold;
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

        public int CalculateQuantityToAdd(List<MonsterViewModel> finalList, MonsterModel monster)
        {
            var experienceRemaining = Encounter.GetPartyDifficulty - GetMonstersExperienceValue(finalList);

            var quantity = 0;
            while (quantity * monster.Xp * ApplyMonsterSizeMultiplier(quantity) < experienceRemaining && quantity < MaximumAmountPerMonster)
            {
                quantity += 1;
            }
            return new Random().Next(quantity / 2, quantity);
        }
    }
}