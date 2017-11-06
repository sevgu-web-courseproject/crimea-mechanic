using System;

namespace DataAccessLayer.Repositories.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryFactory RepositoryFactory { get; }

        T Repository<T>() where T : IRepositoryBase;

        void SaveChanges();
    }
}
