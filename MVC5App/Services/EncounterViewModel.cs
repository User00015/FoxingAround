using System.Collections.Generic;
using System.Linq;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    // ReSharper disable once InconsistentNaming
    public class EncounterViewModel : IEncounterVM
    {

        public List<MonsterViewModel> Monsters { get; set; }
        public PartyDifficulties Party { get; set; }

    }
}