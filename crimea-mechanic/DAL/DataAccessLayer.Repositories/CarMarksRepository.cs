using System.Data.Entity;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class CarMarksRepository : RepositoryBase<CarMark, long>, ICarMarksRepository
    {
        public CarMarksRepository(DbContext context) : base(context)
        {
            
        }
    }
}
