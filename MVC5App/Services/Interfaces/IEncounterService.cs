using MVC5App.Controllers;
using MVC5App.ViewModels;

namespace MVC5App.Services.Interfaces
{
    public interface IEncounterService
    {
        EncounterViewModel Encounter { get; set; }
        void CreateEncounter(IPartyViewModel levels);
        int GetEncounterExperience();
    }
}