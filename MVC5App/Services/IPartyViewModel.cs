using System.Collections.Generic;

namespace MVC5App.Services
{
    public interface IPartyViewModel
    {
       int TotalDeadlyXP { get; } 
       int TotalHardXP { get; } 
       int TotalMediumXP { get; } 
       int TotalEasyXP { get; } 
    }
}