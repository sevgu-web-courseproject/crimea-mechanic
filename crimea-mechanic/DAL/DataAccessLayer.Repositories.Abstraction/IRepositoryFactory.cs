namespace DataAccessLayer.Repositories.Abstraction
{
    public interface IRepositoryFactory
    {
        T Repository<T>() where T : IRepositoryBase;
    }
}
