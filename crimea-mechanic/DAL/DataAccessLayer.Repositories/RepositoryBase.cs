using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Models.Abstraction;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public abstract class RepositoryBase<T, TPrimaryKey> : IRepositoryBase<T, TPrimaryKey> where T : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext _dbContext;
        protected DbSet<T> _entitySet;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entitySet = _dbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll(bool noTracking)
        {
            return noTracking ? _entitySet.AsNoTracking() : _entitySet;
        }

        public T Get(TPrimaryKey key)
        {
            return _entitySet.Find(key);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Add(T entity)
        {
            _entitySet.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            _entitySet.AddRange(entities);
        }

        public bool Delete(TPrimaryKey key)
        {
            var obj = _entitySet.Find(key);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            return true;
        }

        public bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return true;
        }
    }
}
