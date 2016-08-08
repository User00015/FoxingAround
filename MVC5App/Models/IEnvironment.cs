namespace MVC5App.Models
{
    public interface IEnvironment
    {
        string Arctic { get; set; }
        string Coastal { get; set; }
        string Desert { get; set; }
        string Forest { get; set; }
        string Grassland { get; set; }
        string Hill { get; set; }
        string Mountain { get; set; }
        string Swamp { get; set; }
        string Underdark { get; set; }
        string Underwater { get; set; }
        string Urban { get; set; }
    }
}