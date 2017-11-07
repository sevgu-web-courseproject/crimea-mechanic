using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CarModelsRepository : RepositoryBase<CarModel, long>, ICarModelsRepository
    {
        public CarModelsRepository(DbContext context) : base(context)
        {
            
        }
    }
}
