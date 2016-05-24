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
            var finalList = new List<MonsterViewModel>();

            foreach (var monster in _monsterRepository.GetMonsters(encounter).Shuffle())
            {

                //Create monsters until experience 'runs out'
                var experienceRemaining = encounter.GetPartyDifficulty - GetMonstersExperienceValue(finalList);

                //Find the maximum amount of a monster that can be added to an encounter
                var amountToAdd = CalculateQuantityToAdd(experienceRemaining, monster, 3);

                //Randomize how many of that monster to add
                var quantity = new Random().Next(amountToAdd / 2, amountToAdd);


                //Don't include large variations in monster xp value. E.G. 1 Ancient red dragon and 1 cat.
                var threshold = 0;
                if (finalList.Any())
                {
                    threshold = (int)(finalList.Max(m => m.ExperienceValue) * .25);
                }


                if (quantity > 0 && monster.Xp  > threshold)
                    finalList.Add(new MonsterViewModel
                    {
                        Quantity = quantity,
                        ExperienceValue = monster.Xp * quantity,
                        Level = monster.ChallengeRating,
                        Name = monster.Name
                    });

                finalList.RemoveAll(m => m.ExperienceValue < threshold);

                //If the total encounter experience is close to the target difficulty, stop. Prevents 'stuffing' an encounter with increasingly smaller/fewer enemies.
                if (GetMonstersExperienceValue(finalList) > encounter.GetPartyDifficulty * .9)
                    break;
            }

            Encounter.Monsters = finalList.OrderByDescending(m => m.ExperienceValue);
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

        private int CalculateQuantityToAdd(int experienceRemaining, MonsterModel monster, int maxQuantityToAdd = 100)
        {
            var quantity = 0;
            while (quantity * monster.Xp * ApplyMonsterSizeMultiplier(quantity) < experienceRemaining && quantity < maxQuantityToAdd)
            {
                quantity += 1;
            }

            return quantity;
        }
    }
}