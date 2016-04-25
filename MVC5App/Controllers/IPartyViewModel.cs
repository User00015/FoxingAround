namespace MVC5App.Controllers
{
    public interface IPartyViewModel
    {
        int Difficulty { get; set; }
        int PartyLevel { get; set; }
        int PartySize { get; set; }
    }
}