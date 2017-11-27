using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CarServiceFileRepository : RepositoryBase<CarServiceFile, long>, ICarServiceFileRepository
    {
        public CarServiceFileRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
