using System.Collections.Generic;
using MVC5App.ViewModels;

namespace MVC5App.Tests.Controllers
{
    internal static class Generator
    {
        public static List<MonsterViewModel> CreateMonsters(int monstersToAdd)
        {
            if(monstersToAdd == 0) return new List<MonsterViewModel>();

            return new List<MonsterViewModel>()
            {
                new MonsterViewModel()
                {
                    ExperienceValue = 50,
                    Quantity = monstersToAdd
                }
            };
        }
    }
}