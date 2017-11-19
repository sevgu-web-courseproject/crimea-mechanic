﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Application;
using BusinessLogic.Resources;
using Common.Enums;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class ApplicationManager : BaseManagerWithPagination, IApplicationManager
    {

        #region Fields

        private readonly IValidationManager _validationManager;

        #endregion

        #region Constructor

        public ApplicationManager(IUnitOfWork unitOfWork, IUserInternalManager userManager, IValidationManager validationManager) : base(unitOfWork, userManager)
        {
            _validationManager = validationManager;
        }

        #endregion

        #region Implementation of IApplicationManager

        public void CreateApplication(CreateApplicationDto dto, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var validationResult = _validationManager.ValidateCreateApplicationDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var application = Mapper.Map<Application>(dto);

            var car = UnitOfWork.Repository<IUserCarRepository>().Get(dto.CarId);
            if (!car.IsBelongToUser(currentUserId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarDoesNotBelongToUser);
            }

            application.Car = car;
            application.City = UnitOfWork.Repository<ICityRepository>().Get(dto.CityId);

            if (application.Car.Applications == null)
            {
                application.Car.Applications = new List<Application>();
            }

            application.Car.Applications.Add(application);
            UnitOfWork.SaveChanges();
        }

        public void EditApplication(EditApplicationDto dto, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var validationResult = _validationManager.ValidateEditApplicationDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var application = UnitOfWork.Repository<IApplicationRepository>().Get(dto.ApplicationId);
            if (!application.Car.IsBelongToUser(currentUserId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarDoesNotBelongToUser);
            }

            if (application.State != ApplicationState.InSearch)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            var isChanged = false;
            if (application.City.Id != dto.CityId)
            {
                application.City = UnitOfWork.Repository<ICityRepository>().Get(dto.CityId);
                isChanged = true;
            }

            if (!application.Description.Equals(dto.Description))
            {
                application.Description = dto.Description;
                isChanged = true;
            }

            if (isChanged)
            {
                UnitOfWork.SaveChanges();
            }
        }

        public void DeleteApplication(long applicationId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var application = CheckAndGetApplicationForUser(applicationId, currentUserId);

            var statesToRemove = new List<ApplicationState> {ApplicationState.InSearch, ApplicationState.Rejected};
            if (!statesToRemove.Contains(application.State))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            application.IsDeleted = true;
            application.State = ApplicationState.Deleted;
            application.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public ApplicationInfoForUserDto GetApplicationInfoForUser(long applicationId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var application = CheckAndGetApplicationForUser(applicationId, currentUserId);

            return  Mapper.Map<ApplicationInfoForUserDto>(application);
        }

        public ApplicationInfoForServiceDto GetApplicationInfoForService(long applicationId, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            var application = CheckAndGetApplicationForCarService(applicationId, currentUserId);

            return  Mapper.Map<ApplicationInfoForServiceDto>(application);
        }

        public ApplicationInfoForAdministratorDto GetApplicationInfoForAdministrator(long applicationId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);

            var application = UnitOfWork.Repository<IApplicationRepository>().Get(applicationId);
            if (application == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationNotFound);
            }

            return Mapper.Map<ApplicationInfoForAdministratorDto>(application);
        }

        public CollectionResult<ApplicationShortInfoForUserDto> GetApplicationsInfoForUser(ApplicationsFilter filter, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            if (filter == null)
            {
                filter = new ApplicationsFilter
                {
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage,
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage
                };
            }

            var result = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForUser(filter, currentUserId), out var itemsCount)
                .ToList()
                .Select(Mapper.Map<ApplicationShortInfoForUserDto>);

            return new CollectionResult<ApplicationShortInfoForUserDto>
            {
                ItemsCount = itemsCount,
                CurrentPage = filter.CurrentPage,
                ItemsPerPage = filter.ItemsPerPage,
                Items = result
            };

        }

        public CollectionResult<ApplicationShortInfoForServiceDto> GetApplicationsForService(ApplicationsFilter filter, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            if (filter == null)
            {
                filter = new ApplicationsFilter
                {
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage,
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage
                };
            }

            var result = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForCarService(filter, currentUserId), out var itemsCount)
                .ToList()
                .Select(item =>
                {
                    var dto = Mapper.Map<ApplicationShortInfoForServiceDto>(item);
                    dto.IsOfferSended = item.Offers != null && item.Offers.Any(of => !of.IsDeleted && of.Service.ApplicationUser.Id == currentUserId);
                    dto.IsOfferAccepted = item.Service != null && item.Service.ApplicationUser.Id == currentUserId;
                    return dto;
                });

            return new CollectionResult<ApplicationShortInfoForServiceDto>
            {
                ItemsCount = itemsCount,
                CurrentPage = filter.CurrentPage,
                ItemsPerPage = filter.ItemsPerPage,
                Items = result
            };
        }

        public CollectionResult<ApplicationShortInfoForAdministratorDto> GetApplicationsForAdministrator(ApplicationsFilter filter, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);

            if (filter == null)
            {
                filter = new ApplicationsFilter
                {
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage,
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage
                };
            }

            var result = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForAdministrator(filter), out var itemsCount)
                .ToList()
                .Select(Mapper.Map<ApplicationShortInfoForAdministratorDto>);

            return new CollectionResult<ApplicationShortInfoForAdministratorDto>
            {
                ItemsCount = itemsCount,
                CurrentPage = filter.CurrentPage,
                ItemsPerPage = filter.ItemsPerPage,
                Items = result
            };
        }

        public CollectionResult<ApplicationShortInfoForServiceDto> GetApplicationsFromPool(ApplicationsPoolFilter filter, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);
            var service = UnitOfWork.Repository<ICarServiceRepository>().GetByUserId(currentUserId);

            if (filter == null)
            {
                filter = new ApplicationsPoolFilter
                {
                    CurrentPage = Common.Constants.FilterConstants.DefaultCurrentPage,
                    ItemsPerPage = Common.Constants.FilterConstants.DefaultItemsPerPage
                };
            }

            var result = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForPool(service), out var itemsCount)
                .ToList()
                .Select(item =>
                {
                    var dto = Mapper.Map<ApplicationShortInfoForServiceDto>(item);
                    dto.IsOfferSended = item.Offers != null && item.Offers.Any(of => !of.IsDeleted && of.Service.ApplicationUser.Id == currentUserId);
                    dto.IsOfferAccepted = item.Service != null && item.Service.ApplicationUser.Id == currentUserId;
                    return dto;
                });

            return new CollectionResult<ApplicationShortInfoForServiceDto>
            {
                ItemsCount = itemsCount,
                CurrentPage = filter.CurrentPage,
                ItemsPerPage = filter.ItemsPerPage,
                Items = result
            };
        }

        public void AddOffer(AddOfferDto dto, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);
            var user = UserManager.CheckAndGet(currentUserId);

            var validationResult = _validationManager.ValidateAddOfferDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var application = UnitOfWork.Repository<IApplicationRepository>().Get(dto.ApplicationId);
            // Заявка не находится в состоянии поиска
            if (application.State != ApplicationState.InSearch)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            if (application.Offers == null)
            {
                application.Offers = new List<ApplicationOffer>();
            }

            // От автосервиса уже предложение подано
            if (application.Offers.Any(of => of.Service.Id == user.CarService.Id && !of.IsDeleted))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationOfferAlreadyContains);
            }

            var offer = Mapper.Map<ApplicationOffer>(dto);
            offer.Application = application;
            offer.Service = user.CarService;

            application.Offers.Add(offer);
            UnitOfWork.SaveChanges();
        }

        public void DeleteOffer(long offerId, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            var offer = UnitOfWork.Repository<IApplicationOffersRepository>().Get(offerId);
            if (offer == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationOfferNotFound);
            }

            if (offer.Service.ApplicationUser.Id != currentUserId)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationOfferDoesNotBelongToCarService);
            }

            // Заявка не находиться в состоянии поиска
            if (offer.Application.State != ApplicationState.InSearch)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            offer.IsDeleted = true;
            offer.Updated = DateTime.UtcNow;
            
            UnitOfWork.SaveChanges();
        }

        public void AcceptOffer(long offerId, string currentUserId)
        {
            UserManager.IsUserInRegularRole(currentUserId);

            var offer = UnitOfWork.Repository<IApplicationOffersRepository>().Get(offerId);
            if (offer == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationOfferNotFound);
            }

            var application = offer.Application;
            if (!application.Car.IsBelongToUser(currentUserId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationDoesNotBelongToUser);
            }

            // Заявка не находиться в состоянии поиска
            if (application.State != ApplicationState.InSearch)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            application.Service = offer.Service;
            application.State = ApplicationState.InProcessing;
            application.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public void RejectApplication(long applicationId, string currentUserId)
        {
            var isRegular = UserManager.IsUserInRole(currentUserId, Common.Constants.CommonRoles.Regular);
            var isService = UserManager.IsUserInRole(currentUserId, Common.Constants.CommonRoles.CarService);
            if (!isRegular || !isService)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserHasDifferentRole);
            }

            var application = isRegular
                ? CheckAndGetApplicationForUser(applicationId, currentUserId)
                : CheckAndGetApplicationForCarService(applicationId, currentUserId);

            if (application.State != ApplicationState.InProcessing)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            application.State = ApplicationState.Rejected;
            application.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        public void CompleteApplication(long applicationId, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            var application = CheckAndGetApplicationForCarService(applicationId, currentUserId);

            if (application.State != ApplicationState.InProcessing)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationIncorrectState);
            }

            application.State = ApplicationState.Completed;
            application.Updated = DateTime.UtcNow;

            UnitOfWork.SaveChanges();
        }

        #endregion

        #region Private methods

        private Application CheckAndGetApplicationForUser(long applicationId, string userId)
        {
            var application = UnitOfWork.Repository<IApplicationRepository>().Get(applicationId);
            if (application == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationNotFound);
            }

            if (!application.Car.IsBelongToUser(userId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationDoesNotBelongToUser);
            }
            return application;
        }

        private Application CheckAndGetApplicationForCarService(long applicationId, string userId)
        {
            var application = UnitOfWork.Repository<IApplicationRepository>().Get(applicationId);
            if (application == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationNotFound);
            }

            if (!application.IsBelongToService(userId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationDoesNotBelongToCarService);
            }
            return application;
        }

        private IQueryable<Application> BuildQueryForUser(ApplicationsFilter filter, string currentUserId)
        {
            var query = UnitOfWork.Repository<IApplicationRepository>().GetAll(true)
                .Where(app => !app.IsDeleted && app.Car.User.ApplicationUser.Id == currentUserId);

            if (filter.State.HasValue)
            {
                query = query.Where(app => app.State == filter.State.Value);
            }

            return query.OrderByDescending(app => app.Created);
        }

        private IQueryable<Application> BuildQueryForCarService(ApplicationsFilter filter, string currentUserId)
        {
            var query = UnitOfWork.Repository<IApplicationRepository>().GetAll(true)
                .Where(app => !app.IsDeleted && app.Service != null && app.Service.ApplicationUser.Id == currentUserId);

            if (filter.State.HasValue)
            {
                query = query.Where(app => app.State == filter.State.Value);
            }

            return query.OrderByDescending(app => app.Created);
        }

        private IQueryable<Application> BuildQueryForPool(CarService service)
        {
            var query = UnitOfWork.Repository<IApplicationRepository>().GetAll(true)
                .Where(app => !app.IsDeleted && app.State == ApplicationState.InSearch);

            query = query.Where(app => app.City.Id == service.City.Id);

            return query.OrderByDescending(app => app.Created);
        }

        private IQueryable<Application> BuildQueryForAdministrator(ApplicationsFilter filter)
        {
            var query = UnitOfWork.Repository<IApplicationRepository>().GetAll(true)
                .Where(app => !app.IsDeleted);

            if (filter.State.HasValue)
            {
                query = query.Where(app => app.State == filter.State.Value);
            }

            return query.OrderByDescending(app => app.Created);
        }

        #endregion
    }
}