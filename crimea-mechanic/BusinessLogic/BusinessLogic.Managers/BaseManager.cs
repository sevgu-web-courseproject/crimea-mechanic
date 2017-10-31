using BusinessLogic.Managers.Abstraction;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public abstract class BaseManager
    {
        #region Fields

        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IUserInternalManager UserManager;

        #endregion

        #region Constructor

        protected BaseManager(IUnitOfWork unitOfWork, IUserInternalManager userManager)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
        }

        #endregion
    }
}
