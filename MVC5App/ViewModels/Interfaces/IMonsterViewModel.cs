namespace MVC5App.ViewModels.Interfaces
{
    public interface IMonsterViewModel
    {
        int Id { get; set; }
        int ExperienceValue { get; set; }
        string Level { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
    }
}