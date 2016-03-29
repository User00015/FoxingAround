namespace MVC5App.Services
{
    public class EncounterService : IEncounterService
    {
        public bool GetMonster()
        {
            return false;
        }  
    }

    public interface IEncounterService
    {
        bool GetMonster();
    }
}