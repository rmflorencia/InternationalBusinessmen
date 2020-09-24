using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext context;
        protected DbSet<T> entities;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }        

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void RemoveAll()
        {
            context.RemoveRange(GetAll());
            SaveChanges();
        }

        public void AddRange(IEnumerable<T> range)
        {
            context.AddRange(range);
            SaveChanges();
        }

        public void BulkInsert(IList<T> entities)
        {
            context.BulkInsert(entities);
        }       

    }
}
