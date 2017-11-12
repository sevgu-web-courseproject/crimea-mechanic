using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class UserCarRepository : RepositoryBase<UserCar, long>, IUserCarRepository
    {
        public UserCarRepository(DbContext context) : base(context)
        {
            
        }
    }
}
