using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class ApplicationOffersRepository : RepositoryBase<ApplicationOffer, long>, IApplicationOffersRepository
    {
        public ApplicationOffersRepository(DbContext context) : base(context)
        {
            
        }
    }
}
