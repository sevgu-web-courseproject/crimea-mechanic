using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories.Abstraction
{
    public interface ICarServiceRepository : IRepositoryBase<CarService, long>
    {
        CarService GetByUserId(string userId);
    }
}
