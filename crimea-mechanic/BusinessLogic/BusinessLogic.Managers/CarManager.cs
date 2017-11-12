using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Car;
using BusinessLogic.Resources;
using Common.Exceptions;
using DataAccessLayer.Models;
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
            UserManager.IsUserInRegularRole(currentUserId);
            var user = UserManager.CheckAndGet(currentUserId);

            var validationResult = _validationManager.ValidateUserCarDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var userCar = Mapper.Map<UserCar>(dto);
            userCar.Model = UnitOfWork.Repository<ICarModelsRepository>().Get(dto.ModelId.Value);
            user.UserProfile.Cars.Add(userCar);

            UnitOfWork.SaveChanges();
        }

        public void EditCar(AddOrEditUserCarDto dto, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var validationResult = _validationManager.ValidateUserCarDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var repository = UnitOfWork.Repository<IUserCarRepository>();
            var userCar = repository.Get(dto.Id.Value);

            if (userCar.User.ApplicationUser.Id != currentUserId)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarDoesNotBelongToUser);
            }

            var isChanged = false;
            if (!userCar.Name.Equals(dto.Name))
            {
                userCar.Name = dto.Name;
                isChanged = true;
            }
            if (!userCar.Vin.Equals(dto.Vin))
            {
                userCar.Vin = dto.Vin;
                isChanged = true;
            }
            if (!userCar.Year.Equals(dto.Year))
            {
                userCar.Year = dto.Year;
                isChanged = true;
            }
            if (!userCar.FuelType.Equals(dto.FuelType))
            {
                userCar.FuelType = dto.FuelType;
                isChanged = true;
            }
            if (!userCar.EngineCapacity.Equals(dto.EngineCapacity))
            {
                userCar.EngineCapacity = dto.EngineCapacity;
                isChanged = true;
            }

            if (isChanged)
            {
                userCar.Updated = DateTime.UtcNow;
                repository.Update(userCar);
                UnitOfWork.SaveChanges();
            }
        }

        public void DeleteCar(long userCarId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);
            var repository = UnitOfWork.Repository<IUserCarRepository>();

            var userCar = CheckAndGetUserCar(repository, userCarId, currentUserId);

            userCar.IsDeleted = true;
            userCar.Updated = DateTime.UtcNow;
            repository.Update(userCar);
            UnitOfWork.SaveChanges();
        }

        public void RestoreCar(long userCarId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);
            var repository = UnitOfWork.Repository<IUserCarRepository>();

            var userCar = CheckAndGetUserCar(repository, userCarId, currentUserId);

            userCar.IsDeleted = false;
            userCar.Updated = DateTime.UtcNow;
            repository.Update(userCar);
            UnitOfWork.SaveChanges();
        }

        public UserCarDto GetCar(long userCarId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);
            var repository = UnitOfWork.Repository<IUserCarRepository>();

            var userCar = CheckAndGetUserCar(repository, userCarId, currentUserId);

            return Mapper.Map<UserCarDto>(userCar);
        }

        public IEnumerable<UserCarDto> GetCars(FilterUserCar filter, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);
            var user = UserManager.CheckAndGet(currentUserId);

            if (filter == null)
            {
                filter = new FilterUserCar();
            }

            if (filter.Deleted.HasValue && filter.Deleted.Value)
            {
                return Mapper.Map<IEnumerable<UserCarDto>>(user.UserProfile.Cars.Where(car => car.IsDeleted));
            }

            return Mapper.Map<IEnumerable<UserCarDto>>(user.UserProfile.Cars.Where(car => !car.IsDeleted));
        }

        #endregion

        #region Private methods

        private UserCar CheckAndGetUserCar(IUserCarRepository repository, long userCarId, string userId)
        {
            var userCar = repository.Get(userCarId);
            if (userCar == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserCarNotFound);
            }
            if (userCar.User.ApplicationUser.Id != userId)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarDoesNotBelongToUser);
            }
            return userCar;
        }

        #endregion
    }
}
