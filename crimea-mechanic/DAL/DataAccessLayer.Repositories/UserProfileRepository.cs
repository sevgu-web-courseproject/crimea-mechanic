using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class UserProfileRepository : RepositoryBase<UserProfile, long>, IUserProfileRepository
    {
        public UserProfileRepository(DbContext context) : base(context)
        {
            
        }
    }
}
