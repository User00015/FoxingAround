namespace MVC5App.ViewModels
{
    public class DifficultyViewModel : IDifficultyViewModel
    {
        public int Easy { get; set; }
        public int Medium { get; set; }
        public int Hard { get; set; }
        public int Deadly { get; set; }
        public int ExperienceValue { get; set; }
    }
}