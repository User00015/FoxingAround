namespace MVC5App.ViewModels.Interfaces
{
    public interface IPartyViewModel
    {
        int Difficulty { get; set; }
        int PartyLevel { get; set; }
        int PartySize { get; set; }
        int Environment { get; set; }
    }
}