namespace MVC5App.Models
{
    public interface ISpecialAbility
    {
        int AttackBonus { get; set; }
        string Desc { get; set; }
        string Name { get; set; }
    }
}