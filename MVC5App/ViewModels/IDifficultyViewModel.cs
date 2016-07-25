namespace MVC5App.ViewModels
{
    public interface IDifficultyViewModel
    {
        int Deadly { get; set; }
        int Easy { get; set; }
        int ExperienceValue { get; set; }
        int Hard { get; set; }
        int Medium { get; set; }
    }
}