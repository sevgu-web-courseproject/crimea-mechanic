using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Car;
using BusinessLogic.Resources;
using Common.Enums;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class CarManager : BaseManagerWithPagination, ICarManager
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

            if (userCar.Applications.Any(app => !app.IsDeleted &&
                                                app.State == ApplicationState.InProcessing ||
                                                app.State == ApplicationState.InSearch))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserCarCanNotBeRemove);
            }

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

        public UserCarFullDto GetCar(long userCarId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);
            var repository = UnitOfWork.Repository<IUserCarRepository>();
            var userCar = CheckAndGetUserCar(repository, userCarId, currentUserId);
            var dto = Mapper.Map<UserCarFullDto>(userCar);
            var applications = userCar.Applications.Where(app => !app.IsDeleted).OrderByDescending(app => app.Created).ToList();
            dto.Applications = Mapper.Map<IEnumerable<ApplicationForCarHistoryDto>>(applications);
            return dto;
        }

        public CollectionResult<UserCarDto> GetCars(FilterUserCar filter, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            if (filter == null)
            {
                filter = new FilterUserCar
                {
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage,
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage
                };
            }

            var result = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForCars(currentUserId, filter), out var itemsCount)
                .ToList()
                .Select(Mapper.Map<UserCarDto>)
                .ToList();

            return new CollectionResult<UserCarDto>
            {
                CurrentPage = filter.CurrentPage,
                ItemsPerPage = filter.ItemsPerPage,
                Items = result,
                ItemsCount = itemsCount
            };
        }

        #endregion

        #region Private methods

        private UserCar CheckAndGetUserCar(IUserCarRepository repository, long userCarId, string userId)
        {
            var userCar = repository.GetAll(false)
                .Include(u => u.User)
                .Include(u => u.Applications)
                .FirstOrDefault(u => u.Id == userCarId);
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

        private IQueryable<UserCar> BuildQueryForCars(string userId, FilterUserCar filter)
        {
            var query = UnitOfWork.Repository<IUserCarRepository>()
                .GetAll(true)
                .Include(car => car.Model)
                .Include(car => car.Model.Mark)
                .Where(car => car.User.ApplicationUser.Id == userId);

            if (!filter.IsDeleted)
            {
                query = query.Where(car => !car.IsDeleted);
            }

            return query.OrderByDescending(car => car.Created);
        }

        #endregion
    }
}
