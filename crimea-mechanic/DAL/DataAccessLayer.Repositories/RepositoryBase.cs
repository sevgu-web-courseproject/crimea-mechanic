using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Models.Abstraction;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public abstract class RepositoryBase<T, TPrimaryKey> : IRepositoryBase<T, TPrimaryKey> where T : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> EntitySet;

        protected RepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
            EntitySet = DbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll(bool noTracking)
        {
            return noTracking ? EntitySet.AsNoTracking() : EntitySet;
        }

        public virtual T Get(TPrimaryKey key)
        {
            return EntitySet.Find(key);
        }

        public virtual void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Add(T entity)
        {
            EntitySet.Add(entity);
        }

        public virtual void AddRange(List<T> entities)
        {
            EntitySet.AddRange(entities);
        }

        public virtual bool Delete(TPrimaryKey key)
        {
            var obj = EntitySet.Find(key);
            if (obj == null)
            {
                return false;
            }
            EntitySet.Remove(obj);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            DbContext.Entry(entity).State = EntityState.Deleted;
            return true;
        }
    }
}
