using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CarServiceRepository : RepositoryBase<CarService, long>, ICarServiceRepository
    {
        public CarServiceRepository(DbContext context) : base(context)
        {
            
        }
    }
}
