using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class WorkTypeRepository : RepositoryBase<WorkType, long>, IWorkTypeRepository
    {
        public WorkTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
