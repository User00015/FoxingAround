using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MVC5App.Controllers;

namespace MVC5App.Services
{
    public interface IEncounterService
    {
        EncounterViewModel Encounter { get; set; }
        void CreateEncounter(IPartyViewModel levels);
        int GetEncounterExperience();
    }
}