using System.Collections.Generic;

namespace MVC5App.DynamoDb
{
    public interface ITableDataService
    {
        void Store<T>(T item) where T : class;
        void BatchStore<T>(IEnumerable<T> items) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        T GetItem<T>(object key) where T : class;
        void Delete<T>(T item) where T : class;
        void UpdateItem<T>(T item) where T : class;
    }
}