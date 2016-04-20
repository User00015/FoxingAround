using System.Collections.Generic;

namespace MVC5App.Services
{
    public interface IPartyViewModel
    {
        List<Difficulty> Difficulty { get; }
    }
}