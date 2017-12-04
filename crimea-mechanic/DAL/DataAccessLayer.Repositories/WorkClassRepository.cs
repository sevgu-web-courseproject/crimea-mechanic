using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class WorkClassRepository : RepositoryBase<WorkClass, long>, IWorkClassRepository
    {
        public WorkClassRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
