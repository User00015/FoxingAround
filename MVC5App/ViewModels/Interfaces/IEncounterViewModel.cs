using System.Collections.Generic;
using MVC5App.Controllers;
using MVC5App.Services;
using MVC5App.Services.Models;

namespace MVC5App.ViewModels.Interfaces
{
    public interface IEncounterViewModel
    {
        IEnumerable<IMonsterViewModel> Monsters { get; set; }
        Party Party { get; set; }
    }
}