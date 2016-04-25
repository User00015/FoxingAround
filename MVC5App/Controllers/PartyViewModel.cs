namespace MVC5App.Controllers
{
    public class PartyViewModel : IPartyViewModel
    {
        public int PartyLevel { get; set; }
        public int PartySize { get; set; }
        public int Difficulty { get; set; }
    }
}