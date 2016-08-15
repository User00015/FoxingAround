using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using MVC5App.ViewModels;

namespace MVC5App.Models
{
    public class SavedEncountersViewModel
    {
        public string Email { get; set; }
    }


    [DynamoDBTable("SavedEncounters")]
    public class SavedMonsterEncounters
    {
        [DynamoDBHashKey]
        public string Email { get; set; }
        public EncountersList MonsterEncounters { get; set; }
    }

    public class EncountersList
    {
        public List<EncounterViewModel> Encounters { get; set; }
    }
}