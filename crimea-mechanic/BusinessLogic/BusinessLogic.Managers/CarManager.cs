using System.Collections.Generic;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Car;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class CarManager : BaseManager, ICarManager
    {
        #region Fields

        private readonly IValidationManager _validationManager;

        #endregion

        #region Constructor

        public CarManager(IUnitOfWork unitOfWork, IUserInternalManager userManager, IValidationManager validationManager) : base(unitOfWork, userManager)
        {
            _validationManager = validationManager;
        }

        #endregion

        #region Implementation of ICarManager

        public void AddCar(AddOrEditUserCarDto dto, string currentUserId)
        {
            throw new System.NotImplementedException();
        }

        public void EditCar(AddOrEditUserCarDto dto, string currentUserId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCar(long userCarId, string currentUserId)
        {
            throw new System.NotImplementedException();
        }

        public UserCarDto GetCar(long userCarId, string currentUserId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserCarDto> GetCars(string currentUserId)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private methods

        

        #endregion
    }
}
