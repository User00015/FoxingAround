using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using MVC5App.Controllers;

namespace MVC5App.Models
{
    [DynamoDBTable("Monsters")]
    public class MonsterModel
    {
        [DynamoDBHashKey]
        public int Id { get; set; } 
        public int Xp { get; set; }

        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public string Alignment { get; set; }
        public string HitDice { get; set; }
        public string Speed { get; set; }
        public string DamageVulnerabilities { get; set; }
        public string DamageResistances { get; set; }
        public string DamageImmunities { get; set; }
        public string ConditionImmunities { get; set; }
        public string Senses { get; set; }
        public string Languages { get; set; }
        public string ChallengeRating { get; set; }

        public int ArmorClass { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int ConstitutionSave { get; set; }
        public int IntelligenceSave { get; set; }
        public int WisdomSave { get; set; }
        public int History { get; set; }
        public int Perception { get; set; }

        public List<SpecialAbility> SpecialAbilities { get; set; }
        public List<Action> Actions { get; set; } 
        public List<LegendaryAction> LegendaryActions { get; set; }
        public Environment Environment { get; set; }
    }
}