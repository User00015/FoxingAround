using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using MVC5App.ViewModels;

namespace MVC5App.Models
{

    [DynamoDBTable("SavedEncounters")]
    public class SavedEncountersViewModel
    {
        [DynamoDBHashKey]
        public string Email { get; set; }
        public List<EncounterViewModel> Encounters { get; set; }
    }
}