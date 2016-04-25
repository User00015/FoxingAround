using System.Collections.Generic;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    public interface IEncounterVM
    {
        List<MonsterViewModel> Monsters { get; set; }
        PartyDifficulties Party { get; set; }
    }
}