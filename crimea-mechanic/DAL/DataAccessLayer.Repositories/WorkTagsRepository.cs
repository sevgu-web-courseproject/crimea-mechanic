using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class WorkTagsRepository : RepositoryBase<WorkTag, long>, IWorkTagsRepository
    {
        public WorkTagsRepository(DbContext context) : base(context)
        {
            
        }
    }
}
