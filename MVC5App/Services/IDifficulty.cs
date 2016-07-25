namespace MVC5App.Services
{
    public interface IDifficulty
    {
        int Deadly { get; set; }
        int Easy { get; set; }
        int Hard { get; set; }
        int Medium { get; set; }
    }
}