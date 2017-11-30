using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Managers.Abstraction;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    /// <summary>
    /// Базовый менджер с методами для работы с гридами
    /// </summary>
    public class BaseManagerWithPagination : BaseManager
    {
        #region Constructor

        public BaseManagerWithPagination(IUnitOfWork unitOfWork, IUserInternalManager userManager) : base(unitOfWork, userManager)
        {
        }

        #endregion

        protected IQueryable<T> Paginate<T>(int currentPage, int itemsPerPage, IQueryable<T> query, out int itemsCount)
        {
            itemsCount = query.Count();
            return query.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
        }

        protected IEnumerable<T> Paginate<T>(int currentPage, int itemsPerPage, IEnumerable<T> query, out int itemsCount)
        {
            itemsCount = query.Count();
            return query.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
        }

    }
}
