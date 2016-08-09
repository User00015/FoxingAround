using MVC5App.Models;

namespace MVC5App.Services.Interfaces
{
    public interface IPartyService
    {
        int TotalDeadlyXP { get; }
        int TotalHardXP { get; }
        int TotalMediumXP { get; }
        int TotalEasyXP { get; }

        int CurrentDifficulty();
        int GetDifficulty(int difficultyType);
        int Difficulty { get;  }
        int Environment { get; set; }
    }
}