using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Ajax.Utilities;
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

        public EncounterViewModel Encounter { get; set; } = new EncounterViewModel();

        private readonly IMonsterRepository _monsterRepository;
        private IPartyService _partyService;
        public int MaximumAmountPerMonster { get; set; } = 5;
        public double MinimumExperienceThreshold { get; set; } = .25;
        public double VariableEncounterExperienceThreshold { get; set; } = .9;
        public IRandomNumber RandomNumber = new RandomNumber();

        public EncounterService(IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
        }

        public void CreateEncounter(IPartyViewModel party)
        {
            _partyService = new PartyService(party);

            Encounter.Monsters = MonsterResolver(_partyService).OrderByDescending(p => p.ExperienceValue).ToList();
            Encounter.Difficulty = new DifficultyViewModel
            {
                ExperienceValue = GetEncountersExperienceValue(Encounter.Monsters),
                Easy = _partyService.TotalEasyXP,
                Medium = _partyService.TotalMediumXP,
                Hard = _partyService.TotalHardXP,
                Deadly = _partyService.TotalDeadlyXP
            };
            Encounter.EncounterExperience = GetEncountersExperienceValue(Encounter.Monsters);
        }

        public IEnumerable<MonsterViewModel> MonsterResolver(IPartyService party)
        {
            var finalList = new List<MonsterViewModel>();

            var monsters = _monsterRepository.GetMonsters(party).Where(p => p.Environment.HasEnvironment(party.Environment)).Shuffle();
            foreach (var monster in monsters)
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
                if (GetEncountersExperienceValue(finalList) > _partyService.CurrentDifficulty() * VariableEncounterExperienceThreshold)
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


        public int GetEncountersExperienceValue(IEnumerable<MonsterViewModel> monsters)
        {
            var monsterViewModels = monsters as IList<MonsterViewModel> ?? monsters.ToList();
            var numberOfMonsters = monsterViewModels.Aggregate(0, (current, model) => current + model.Quantity);
            return (int)(monsterViewModels.Sum(m => m.ExperienceValue * m.Quantity) * ApplyMonsterSizeMultiplier(numberOfMonsters));
        }

        public int CalculateQuantityToAdd(IList<MonsterViewModel> finalList, IMonsterModel monster)
        {

            //Don't change finalList. 
            var tempList = new List<MonsterViewModel>(finalList)
            {
                new MonsterViewModel
                {
                    Quantity = 0,
                    ExperienceValue = monster.Xp,
                    Level = monster.ChallengeRating,
                    Name = monster.Name,
                    Id = monster.Id
                }
            };

            //Start adding copies of creature until maximum is reached.
            while (GetEncountersExperienceValue(tempList) < _partyService.CurrentDifficulty() && tempList.Single(m => m.Id == monster.Id).Quantity < MaximumAmountPerMonster)
            {
                tempList.Single(m => m.Id == monster.Id).Quantity += 1;
            }

            return RandomNumber.Next(tempList.Single(m => m.Id == monster.Id).Quantity - 1);

        }
    }
}