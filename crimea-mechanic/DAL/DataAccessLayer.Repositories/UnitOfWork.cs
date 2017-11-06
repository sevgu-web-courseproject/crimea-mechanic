using System.Data.Entity;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbcontext;
        public IRepositoryFactory RepositoryFactory { get; }

        public UnitOfWork(DbContext dbcontext, IRepositoryFactory repositoryFactory)
        {
            _dbcontext = dbcontext;
            RepositoryFactory = repositoryFactory;
        }

        public T Repository<T>() where T : IRepositoryBase
        {
            return RepositoryFactory.Repository<T>();
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
