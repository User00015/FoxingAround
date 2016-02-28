using Amazon.DynamoDBv2.DataModel;

namespace MVC5App.Controllers
{
    [DynamoDBTable("Test")]
    public class StudentVM
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}