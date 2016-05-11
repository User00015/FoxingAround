using MVC5App.ViewModels.Interfaces;

namespace MVC5App.ViewModels
{
    public class PartyViewModel : IPartyViewModel
    {
        public int PartyLevel { get; set; }
        public int PartySize { get; set; }
        public int Difficulty { get; set; }
    }
}