using System.Collections.Generic;

namespace MVC5App.Services
{
    public interface IPartyDifficulties
    {
       int TotalDeadlyXP { get; } 
       int TotalHardXP { get; } 
       int TotalMediumXP { get; } 
       int TotalEasyXP { get; } 
    }
}