using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CarServiceReviewRepository : RepositoryBase<CarServiceReview, long> , ICarServiceReviewRepository
    {
        public CarServiceReviewRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
