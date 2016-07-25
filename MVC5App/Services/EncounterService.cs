using System;
using System.Collections.Generic;
using System.Linq;
using MVC5App.Extensions;
using MVC5App.Models;
using MVC5App.Repositories.Interfaces;
using MVC5App.Services.Interfaces;
using MVC5App.ViewModels;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.Services
{
    public class EncounterService : IEncounterService
    {
        private const int Single = 1;
        private const int Pair = 2;
        private static readonly IEnumerable<int> GroupSize = Enumerable.Range(3, 4);
        private static readonly IEnumerable<int> GangSize = Enumerable.Range(7, 4);
        private static readonly IEnumerable<int> MobSize = Enumerable.Range(11, 4);
        private static readonly IEnumerable<int> HordeSize = Enumerable.Range(15, 100);

        public EncounterViewModel Encounter { get; set; }

        private readonly IMonsterRepository _monsterRepository;
        private PartyService _partyService;
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
               PartyViewModel = party
            };

            _partyService = new PartyService(party);


            Encounter.Monsters = MonsterResolver(_partyService).OrderByDescending(p => p.ExperienceValue);

            Encounter.Difficulty = new DifficultyViewModel
            {
                ExperienceValue = GetMonstersExperienceValue(Encounter.Monsters),
                Easy = _partyService.TotalEasyXP,
                Medium = _partyService.TotalMediumXP,
                Hard = _partyService.TotalHardXP,
                Deadly = _partyService.TotalDeadlyXP
            };
        }

        public IEnumerable<MonsterViewModel> MonsterResolver(IPartyService party)
        {
            var finalList = new List<MonsterViewModel>();

            foreach (var monster in _monsterRepository.GetMonsters(party).Shuffle())
            {
                //Find the amount of a monster to be added to an encounter
                var quantity = CalculateQuantityToAdd(finalList, monster);

                if (quantity > 0)
                {
                    finalList.Add(new MonsterViewModel
                    {
                        Quantity = quantity,
                        ExperienceValue = monster.Xp,
                        Level = monster.ChallengeRating,
                        Name = monster.Name,
                        Id = monster.Id
                    });

                    //Strip monsters with large value disparities. E.g. 1 Ancient red dragon and 1 cat.
                    finalList.RemoveAll(m => m.ExperienceValue < XpThreshold(finalList));
                }

                //If the total encounter experience is close to the target difficulty, stop. Prevents 'stuffing' an encounter with increasingly smaller/fewer enemies.
                if (GetMonstersExperienceValue(finalList) > _partyService.GetCurrentDifficulty() * VariableEncounterExperienceThreshold)
                    break;
            }

            return finalList;
        }

        private double XpThreshold(IEnumerable<MonsterViewModel> finalList)
        {
            return finalList.Max(p => p.ExperienceValue) * MinimumExperienceThreshold;
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
            if (GroupSize.Contains(monsters))
            {
                return 2;
            }
            if (GangSize.Contains(monsters))
            {
                return 2.5;
            }
            if (MobSize.Contains(monsters))
            {
                return 3;
            }
            if (HordeSize.Contains(monsters))
            {
                return 4;
            }
            return 0;
        }


        public int GetMonstersExperienceValue(IEnumerable<MonsterViewModel> monsters)
        {
            return (int)monsters.Sum(m => m.ExperienceValue * m.Quantity * ApplyMonsterSizeMultiplier(m.Quantity));
        }

        public int CalculateQuantityToAdd(List<MonsterViewModel> finalList, MonsterModel monster)
        {
            var experienceRemaining = _partyService.GetCurrentDifficulty() - GetMonstersExperienceValue(finalList);

            var quantity = 0;
            while (quantity * monster.Xp * ApplyMonsterSizeMultiplier(quantity) < experienceRemaining && quantity < MaximumAmountPerMonster)
            {
                quantity += 1;
            }
            return new Random().Next(quantity - 1, quantity);
        }
    }
}