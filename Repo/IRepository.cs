using System.Collections.Generic;

namespace Repo
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        
        void RemoveAll();

        void SaveChanges();

        void AddRange(IEnumerable<T> range);

        void BulkInsert(IList<T> entities);
    }
}
