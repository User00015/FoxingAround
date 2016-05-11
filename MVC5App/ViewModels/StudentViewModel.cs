using System;
using Amazon.DynamoDBv2.DataModel;

namespace MVC5App.ViewModels
{
    [DynamoDBTable("Test")]
    public class StudentViewModel
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }
}