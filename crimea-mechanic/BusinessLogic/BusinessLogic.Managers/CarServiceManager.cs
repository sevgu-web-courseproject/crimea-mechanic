using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.CarService;
using BusinessLogic.Objects.Review;
using BusinessLogic.Objects.Works;
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

        public void Edit(EditCarServiceDto dto, string directory, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            var service = GetCarService(dto.Id);
            if (service.ApplicationUser.Id != currentUserId)
            {
                DeleteDirectoryWithFiles(directory);
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceDoesNotBelongToUser);
            }

            var validationResult = _validationManager.ValidateEditCarServiceDto(dto);
            if (validationResult.HasErrors)
            {
                DeleteDirectoryWithFiles(directory);
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            service.Name = dto.Name;
            service.Address = dto.Address;
            service.Email = dto.Email;

            if (dto.Logo != null)
            {
                var logo = service.Files.SingleOrDefault(file => !file.IsDeleted && file.Type == FileType.Logo);
                if (logo != null)
                {
                    logo.IsDeleted = true;
                    logo.Updated = DateTime.UtcNow;
                }
                logo = Mapper.Map<CarServiceFile>(dto.Logo);
                logo.Type = FileType.Logo;
                service.Files.Add(logo);
            }

            if (dto.Photos != null && dto.Photos.Any())
            {
                var photos = service.Files.Where(file => !file.IsDeleted && file.Type == FileType.Photo).ToList();
                if (photos.Any())
                {
                    foreach (var photo in photos)
                    {
                        photo.IsDeleted = true;
                        photo.Updated = DateTime.UtcNow;
                    }
                }
                foreach (var photo in dto.Photos)
                {
                    var file = Mapper.Map<CarServiceFile>(photo);
                    service.Files.Add(file);
                }
            }

            foreach (var phone in service.Phones)
            {
                phone.IsDeleted = true;
                phone.Updated = DateTime.UtcNow;
            }
            service.Phones = Mapper.Map<List<CarServicePhone>>(dto.Phones);

            if (dto.WorkTypes != null && dto.WorkTypes.Any())
            {
                var repository = UnitOfWork.Repository<IWorkTypeRepository>();
                service.WorkTypes.Clear();
                foreach (var workTypeId in dto.WorkTypes)
                {
                    service.WorkTypes.Add(repository.Get(workTypeId));
                }
            }

            service.ManagerName = dto.ManagerName;
            service.Site = dto.Site;
            service.TimetableWorks = dto.TimetableWorks;
            service.About = dto.About;
            service.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public CarServiceInfoForEditDto GetInfoForEdit(string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);
            var service = UserManager.CheckAndGet(currentUserId).CarService;
            return Mapper.Map<CarServiceInfoForEditDto>(service);
        }

        public CarServiceInfoDto GetProfile(string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);
            var service = UserManager.CheckAndGet(currentUserId).CarService;

            return GetCarServiceInfo(service);
        }

        public CarServiceInfoDto GetInfo(long carServiceId, string currentUserId)
        {
            var service = GetCarService(carServiceId);

            var infoDto = GetCarServiceInfo(service);

            if (!string.IsNullOrEmpty(currentUserId) && UserManager.IsUserInRole(currentUserId, Common.Constants.CommonRoles.Regular))
            {
                infoDto.ReviewId = service.Reviews.SingleOrDefault(x => !x.IsDeleted && x.User.ApplicationUser.Id == currentUserId)?.Id;
            }

            return infoDto;
        }

        public RegistrationRequestInfoDto GetRegistrationRequest(long carServiceId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);

            var service = GetCarService(carServiceId);

            if (service.State != CarServiceState.UnderСonsideration)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceIncorrectState);
            }

            return Mapper.Map<RegistrationRequestInfoDto>(service);
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
                    dto.AverageMark = item.Reviews.Count(r => !r.IsDeleted) == 0
                        ? 0
                        : item.Points / item.Reviews.Count(r => !r.IsDeleted);
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

        public CollectionResult<RegistrationRequestShortInfoDto> GetRegistrationRequests(CarServiceRegistrationsFilter filter, string currentUserId)
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
                .Select(Mapper.Map<RegistrationRequestShortInfoDto>)
                .ToList();

            return new CollectionResult<RegistrationRequestShortInfoDto>
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

        public void UnBlockCarService(long carServiceId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var service = GetCarService(carServiceId);

            if (service.State != CarServiceState.Blocked)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceIncorrectState);
            }

            service.State = CarServiceState.Active;
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
                .Include(q => q.Reviews);

            if (filter.ShowBlocked.HasValue && filter.ShowBlocked.Value)
            {
                query = query.Where(q => q.State == CarServiceState.Active || q.State == CarServiceState.Blocked);
            }
            else
            {
                query = query.Where(q => q.State == CarServiceState.Active);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(q => q.Name.Contains(filter.Name));
            }

            if (filter.CityId.HasValue)
            {
                query = query.Where(q => q.City.Id == filter.CityId.Value);
            }

            if (filter.Stars.HasValue)
            {
                query = query.Where(q => q.Reviews.Count(r => !r.IsDeleted) != 0 && q.Points / q.Reviews.Count(r => !r.IsDeleted) == filter.Stars.Value);
            }

            return query.OrderByDescending(q => q.Reviews.Count(r => !r.IsDeleted) == 0 
                ? 0
                : q.Points / q.Reviews.Count(r => !r.IsDeleted));
        }

        private IQueryable<CarService> BuildQueryForRegistrationRequests(CarServiceRegistrationsFilter filter)
        {
            var query = UnitOfWork.Repository<ICarServiceRepository>()
                .GetAll(true)
                .Include(q => q.City)
                .Include(q => q.Files)
                .Where(cr => cr.State == CarServiceState.UnderСonsideration)
                .OrderByDescending(cr => cr.Created);

            return query;
        }

        private CarServiceInfoDto GetCarServiceInfo(CarService service)
        {
            var infoDto = Mapper.Map<CarServiceInfoDto>(service);
            infoDto.Reviews = service.Reviews
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.Created)
                .Select(Mapper.Map<ReviewInfoDto>)
                .ToList();
            infoDto.PhotosId = service.Files
                .Where(file => file.Type == FileType.Photo)
                .Select(file => file.Id)
                .ToList();
            infoDto.Phones = service.Phones
                .Select(p => p.Number)
                .ToList();
            infoDto.AverageMark = service.Reviews.Count(r => !r.IsDeleted) == 0
                ? 0
                : service.Points / service.Reviews.Count(r => !r.IsDeleted);
            infoDto.WorkClasses = service.WorkTypes
                .GroupBy(x => x.Class)
                .Select(x =>
                {
                    var dto = Mapper.Map<WorkClassDto>(x.Key);
                    dto.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(x.ToList());
                    return dto;
                })
                .ToList();
            return infoDto;
        }

        private void DeleteDirectoryWithFiles(string directory)
        {
            var filesDirInfo = new DirectoryInfo(directory);
            if (filesDirInfo.Exists)
            {
                filesDirInfo.Delete(true);
            }
        }

        #endregion

    }
}
