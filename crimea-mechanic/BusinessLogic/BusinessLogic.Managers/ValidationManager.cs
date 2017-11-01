using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using Common.Validation;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class ValidationManager : BaseManager, IValidationManager
    {
        #region Constructor

        public ValidationManager(IUnitOfWork unitOfWork, IUserInternalManager userManager)
            : base(unitOfWork, userManager)
        {
        }

        #endregion

        #region Implementation of IValidationManager

        public ValidationResult ValidateRegistrationDto(RegistrationDto dto)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private methods

        

        #endregion
    }
}
