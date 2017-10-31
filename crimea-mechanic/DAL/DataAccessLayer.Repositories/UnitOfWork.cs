using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbcontext;
        public IRepositoryFactory RepositoryFactory { get; }

        public UnitOfWork(DatabaseContext dbcontext, IRepositoryFactory repositoryFactory)
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
