using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.Services;
using MVC5App.Services.Models;
using MVC5App.ViewModels.Interfaces;

namespace MVC5App.ViewModels
{
    // ReSharper disable once InconsistentNaming
    public class EncounterViewModel : IEncounterViewModel
    {
        public List<MonsterViewModel> Monsters { get; set; }
        public Party Party { get; set; }

    }
}