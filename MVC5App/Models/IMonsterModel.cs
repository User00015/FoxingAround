using System.Collections.Generic;

namespace MVC5App.Models
{
    public interface IMonsterModel
    {
        List<Action> Actions { get; set; }
        string Alignment { get; set; }
        int ArmorClass { get; set; }
        string ChallengeRating { get; set; }
        int Charisma { get; set; }
        string ConditionImmunities { get; set; }
        int Constitution { get; set; }
        int ConstitutionSave { get; set; }
        string DamageImmunities { get; set; }
        string DamageResistances { get; set; }
        string DamageVulnerabilities { get; set; }
        int Dexterity { get; set; }
        Environment Environment { get; set; }
        int History { get; set; }
        string HitDice { get; set; }
        int HitPoints { get; set; }
        int Id { get; set; }
        int Intelligence { get; set; }
        int IntelligenceSave { get; set; }
        string Languages { get; set; }
        List<LegendaryAction> LegendaryActions { get; set; }
        string Name { get; set; }
        int Perception { get; set; }
        string Senses { get; set; }
        string Size { get; set; }
        List<SpecialAbility> SpecialAbilities { get; set; }
        string Speed { get; set; }
        int Strength { get; set; }
        string Subtype { get; set; }
        string Type { get; set; }
        int Wisdom { get; set; }
        int WisdomSave { get; set; }
        int Xp { get; set; }
    }
}