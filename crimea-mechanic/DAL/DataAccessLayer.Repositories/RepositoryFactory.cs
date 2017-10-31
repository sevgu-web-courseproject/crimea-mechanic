using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public T Repository<T>() where T : IRepositoryBase
        {
            return GlobalContainer.Container.Instance.Resolve<T>();
        }
    }
}
