using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CityRepository : RepositoryBase<City, long>, ICityRepository
    {
        public CityRepository(DbContext context) : base(context)
        {
            
        }
    }
}
