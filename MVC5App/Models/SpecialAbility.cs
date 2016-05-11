namespace MVC5App.Models
{
    public class SpecialAbility : ISpecialAbility
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int AttackBonus { get; set; }
        public int DamageBonus { get; set; }

    }
}