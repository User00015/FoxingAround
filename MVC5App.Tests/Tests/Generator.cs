using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.Services;
using MVC5App.ViewModels;

namespace MVC5App.Tests.Controllers
{
    internal static class Generator
    {
        public static List<MonsterViewModel> CreateMonsters(int monstersToAdd)
        {
            var mockMonster = new List<MonsterViewModel>();

            for (int i = 0; i < monstersToAdd; i++)
            {
                mockMonster.Add(new MonsterViewModel
                {
                    Name = "Added Monster",
                    Level = "1",
                    ExperienceValue = 50
                });

            }

            return mockMonster;
        }
    }
}