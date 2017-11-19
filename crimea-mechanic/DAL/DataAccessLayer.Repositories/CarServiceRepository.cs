using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CarServiceRepository : RepositoryBase<CarService, long>, ICarServiceRepository
    {
        public CarServiceRepository(DbContext context) : base(context)
        {
            
        }

        public CarService GetByUserId(string userId)
        {
            return GetAll(true)
                .SingleOrDefault(cr => cr.ApplicationUser.Id == userId);
        }
    }
}
