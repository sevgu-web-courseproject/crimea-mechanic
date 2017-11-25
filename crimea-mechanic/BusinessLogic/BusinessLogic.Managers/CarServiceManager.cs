using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.CarService;
using BusinessLogic.Resources;
using Common.Enums;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class CarServiceManager : BaseManagerWithPagination, ICarServiceManager
    {

        #region Fields

        private readonly IValidationManager _validationManager;

        #endregion

        #region Constructor

        public CarServiceManager(IUnitOfWork unitOfWork, IUserInternalManager userManager, IValidationManager validationManager) : base(unitOfWork, userManager)
        {
            _validationManager = validationManager;
        }

        #endregion

        #region Implemetation of ICarServiceManager

        public void Edit(EditCarServiceDto dto, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            var service = GetCarService(dto.Id);
            if (service.ApplicationUser.Id != currentUserId)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceDoesNotBelongToUser);
            }

            var validationResult = _validationManager.ValidateEditCarServiceDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            service.Name = dto.Name;
            service.City = UnitOfWork.Repository<ICityRepository>().Get(dto.CityId);
            service.Address = dto.Address;
            service.Email = dto.Email;

            //TODO Телефоны и фотки

            service.ManagerName = dto.ManagerName;
            service.Site = dto.Site;
            service.TimetableWorks = dto.TimetableWorks;
            service.About = dto.About;
            service.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public CarServiceInfoDto GetInfo(long carServiceId)
        {
            var service = GetCarService(carServiceId);

            var infoDto = Mapper.Map<CarServiceInfoDto>(service);
            infoDto.PhotosId = service.Files
                .Where(file => file.Type == FileType.Photo)
                .Select(file => file.Id)
                .ToList();
            infoDto.Phones = service.Phones
                .Select(p => p.Number)
                .ToList();
            infoDto.AverageMark = service.Reviews.Count == 0
                ? 0
                : service.Points / service.Reviews.Count;

            return infoDto;
        }

        public CarServiceInfoDto GetRegistrationRequest(long carServiceId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);

            var service = GetCarService(carServiceId);

            if (service.State != CarServiceState.UnderСonsideration)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceIncorrectState);
            }

            var infoDto = Mapper.Map<CarServiceInfoDto>(service);
            infoDto.PhotosId = service.Files
                .Where(file => file.Type == FileType.Photo)
                .Select(file => file.Id)
                .ToList();

            return infoDto;
        }

        public CollectionResult<CarServiceShortInfoDto> GetInfos(CarServiceFilter filter)
       {
            if (filter == null)
            {
                filter = new CarServiceFilter
                {
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage,
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage
                };
            }

            var infos = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForCarServices(filter), out var itemsCount)
                .ToList()
                .Select(item =>
                {
                    var dto = Mapper.Map<CarServiceShortInfoDto>(item);
                    dto.AverageMark = item.Reviews.Count == 0
                        ? 0
                        : item.Points / item.Reviews.Count;
                    return dto;
                })
                .ToList();

            return new CollectionResult<CarServiceShortInfoDto>
            {
                CurrentPage = filter.CurrentPage,
                Items = infos,
                ItemsCount = itemsCount,
                ItemsPerPage = filter.ItemsPerPage
            };
        }

        public CollectionResult<CarServiceShortInfoDto> GetRegistrationRequests(CarServiceRegistrationsFilter filter, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);

            if (filter == null)
            {
                filter = new CarServiceRegistrationsFilter
                {
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage,
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage
                };
            }

            var infos = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForRegistrationRequests(filter), out var itemsCount)
                .ToList()
                .Select(Mapper.Map<CarServiceShortInfoDto>);

            return new CollectionResult<CarServiceShortInfoDto>
            {
                CurrentPage = filter.CurrentPage,
                Items = infos,
                ItemsCount = itemsCount,
                ItemsPerPage = filter.ItemsPerPage
            };
        }

        public void ApproveCarService(long carServiceId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var service = GetCarService(carServiceId);

            if (service.State != CarServiceState.UnderСonsideration)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceIncorrectState);
            }

            service.State = CarServiceState.Active;
            service.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public void RejectCarService(long carServiceId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var service = GetCarService(carServiceId);

            if (service.State != CarServiceState.UnderСonsideration)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceIncorrectState);
            }

            service.State = CarServiceState.Rejected;
            service.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public void BlockCarService(long carServiceId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var service = GetCarService(carServiceId);

            if (service.State != CarServiceState.Active)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceIncorrectState);
            }

            service.State = CarServiceState.Blocked;
            service.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        #endregion

        #region Private methods

        private CarService GetCarService(long carServiceId)
        {
            var service = UnitOfWork.Repository<ICarServiceRepository>().Get(carServiceId);
            if (service == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceNotFound);
            }
            return service;
        }

        private IQueryable<CarService> BuildQueryForCarServices(CarServiceFilter filter)
        {
            var query = UnitOfWork.Repository<ICarServiceRepository>()
                .GetAll(true)
                .Include(q => q.Reviews)
                .Include(q => q.City)
                .Include(q => q.Files)
                .Where(cr => cr.State == CarServiceState.Active)
                .OrderByDescending(cr => cr.Points);

            return query;
        }

        private IQueryable<CarService> BuildQueryForRegistrationRequests(CarServiceRegistrationsFilter filter)
        {
            var query = UnitOfWork.Repository<ICarServiceRepository>()
                .GetAll(true)
                .Where(cr => cr.State == CarServiceState.UnderСonsideration)
                .OrderByDescending(cr => cr.Created);

            return query;
        }

        #endregion

    }
}
