using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models.Abstraction;

namespace DataAccessLayer.Repositories.Abstraction
{
    public interface IRepositoryBase { }

    public interface IRepositoryBase<T, TPrimaryKey> : IRepositoryBase where T : IEntity<TPrimaryKey>
    {
        IQueryable<T> GetAll(bool noTracking);

        T Get(TPrimaryKey key);

        Task<T> GetAsync(TPrimaryKey key);

        void Update(T entity);

        void Add(T entity);

        void AddRange(List<T> entities);

        bool Delete(TPrimaryKey key);

        bool Delete(T entity);
    }
}
