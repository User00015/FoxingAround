using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace MVC5App.DynamoDb
{
    public class DynamoService : ITableDataService
    {
        public readonly DynamoDBContext Context;
        public AmazonDynamoDBClient Client;

        public DynamoService()
        {
            Client = new AmazonDynamoDBClient();
            Context = new DynamoDBContext(Client, new DynamoDBContextConfig
            {
                ConsistentRead = true,
                SkipVersionCheck = true
            });
        }

        public void Store<T>(T item) where T : class
        {
            Context.Save(item);
        }

        public void BatchStore<T>(IEnumerable<T> items) where T : class
        {
            var itemBatch = Context.CreateBatchWrite<T>();
            itemBatch.AddPutItems(items);
            itemBatch.Execute();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return Context.Scan<T>();
        }

        public T GetItem<T>(object key) where T : class
        {
            return Context.Load<T>(key);
        }

        public void UpdateItem<T>(T item) where T : class
        {
            var savedItem = Context.Load(item);

            if (savedItem == null)
            {
                throw new AmazonDynamoDBException("The item does not exist in the table");
            }

            Context.Save(item);
        }

        public void Delete<T>(T item) where T : class
        {
            var savedItem = Context.Load(item);

            if (savedItem == null)
            {
                throw new AmazonDynamoDBException("The item does not exist in the table");
            }

            Context.Delete(item);
        }
    }
}