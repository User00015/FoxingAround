using MVC5App.ViewModels.Interfaces;

namespace MVC5App.ViewModels
{
    public class MonsterViewModel : IMonsterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public int ExperienceValue { get; set; }
        public int Quantity { get; set; }
    }
}