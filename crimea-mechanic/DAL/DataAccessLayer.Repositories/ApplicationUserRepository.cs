using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser, string>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DbContext dbContext) : base(dbContext) { }
    }
}
