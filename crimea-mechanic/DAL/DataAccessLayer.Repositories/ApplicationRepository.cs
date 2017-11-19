using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class ApplicationRepository : RepositoryBase<Application, long>, IApplicationRepository
    {
        public ApplicationRepository(DbContext context) : base(context)
        {
            
        }
    }
}
