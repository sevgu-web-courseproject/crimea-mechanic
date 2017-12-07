using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Application;
using BusinessLogic.Objects.Works;
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

            if (dto.WorkTypes != null && dto.WorkTypes.Any())
            {
                var repository = UnitOfWork.Repository<IWorkTypeRepository>();
                application.WorkTypes = new List<WorkType>();
                foreach (var work in dto.WorkTypes)
                {
                    application.WorkTypes.Add(repository.Get(work));
                }
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

            application.Description = dto.Description;
            UnitOfWork.SaveChanges();
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

            var dto = Mapper.Map<ApplicationInfoForUserDto>(application);
            if (application.Offers != null)
            {
                dto.Offers = application.Offers
                    .Where(of => !of.IsDeleted)
                    .OrderByDescending(of => of.Created)
                    .Select(Mapper.Map<OfferInfoDto>)
                    .ToList();
            }
            dto.WorkClasses = application.WorkTypes
                    .GroupBy(x => x.Class)
                    .Select(x =>
                    {
                        var info = Mapper.Map<WorkClassDto>(x.Key);
                        info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(x.ToList());
                        return info;
                    })
                    .ToList();

            return dto;
        }

        public ApplicationInfoForServiceDto GetApplicationInfoForService(long applicationId, string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);

            var application = CheckAndGetApplicationForCarService(applicationId, currentUserId);

            var dto = Mapper.Map<ApplicationInfoForServiceDto>(application);
            dto.WorkClasses = application.WorkTypes
                .GroupBy(x => x.Class)
                .Select(x =>
                {
                    var info = Mapper.Map<WorkClassDto>(x.Key);
                    info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(x.ToList());
                    return info;
                })
                .ToList();
            return  dto;
        }

        public ApplicationInfoForAdministratorDto GetApplicationInfoForAdministrator(long applicationId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);

            var application = UnitOfWork.Repository<IApplicationRepository>().Get(applicationId);
            if (application == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.ApplicationNotFound);
            }
            var dto = Mapper.Map<ApplicationInfoForAdministratorDto>(application);
            if (application.Offers != null)
            {
                dto.Offers = application.Offers
                    .Where(of => !of.IsDeleted)
                    .OrderByDescending(of => of.Created)
                    .Select(Mapper.Map<OfferInfoDto>)
                    .ToList();
            }
            dto.WorkClasses = application.WorkTypes
                .GroupBy(x => x.Class)
                .Select(x =>
                {
                    var info = Mapper.Map<WorkClassDto>(x.Key);
                    info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(x.ToList());
                    return info; 
                })
                .ToList();

            return dto;
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
                .Select(x =>
                {
                    var dto = Mapper.Map<ApplicationShortInfoForUserDto>(x);
                    dto.WorkClasses = x.WorkTypes
                        .GroupBy(y => y.Class)
                        .Select(y =>
                        {
                            var info = Mapper.Map<WorkClassDto>(y.Key);
                            info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(y.ToList());
                            return info;
                        })
                        .ToList();
                    return dto;
                });

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
                    dto.OfferId = item.Offers.FirstOrDefault(of => !of.IsDeleted && of.Service.ApplicationUser.Id == currentUserId)?.Id;
                    dto.WorkClasses = item.WorkTypes
                        .GroupBy(y => y.Class)
                        .Select(y =>
                        {
                            var info = Mapper.Map<WorkClassDto>(y.Key);
                            info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(y.ToList());
                            return info;
                        })
                        .ToList();
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
                .Select(x =>
                {
                    var dto = Mapper.Map<ApplicationShortInfoForAdministratorDto>(x);
                    dto.WorkClasses = x.WorkTypes
                        .GroupBy(y => y.Class)
                        .Select(y =>
                        {
                            var info = Mapper.Map<WorkClassDto>(y.Key);
                            info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(y.ToList());
                            return info;
                        })
                        .ToList();
                    return dto;
                });

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

            var result = Paginate(filter.CurrentPage, filter.ItemsPerPage, BuildQueryForPool(filter, service), out var itemsCount)
                .ToList()
                .Select(item =>
                {
                    var dto = Mapper.Map<ApplicationShortInfoForServiceDto>(item);
                    dto.OfferId = item.Offers.FirstOrDefault(of => !of.IsDeleted && of.Service.ApplicationUser.Id == currentUserId)?.Id;
                    dto.WorkClasses = item.WorkTypes
                        .GroupBy(y => y.Class)
                        .Select(y =>
                        {
                            var info = Mapper.Map<WorkClassDto>(y.Key);
                            info.Types = Mapper.Map<IEnumerable<WorkTypeDto>>(y.ToList());
                            return info;
                        })
                        .ToList();
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
            if (!isRegular && !isService)
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

        public IEnumerable<CarMarkDto> GetAvailableMarksFromPool(string currentUserId)
        {
            UserManager.IsUserInCarServiceRole(currentUserId);
            var service = UnitOfWork.Repository<ICarServiceRepository>().GetByUserId(currentUserId);

            return BuildQueryForPool(new ApplicationsPoolFilter(), service)
                .Select(x => x.Car.Model.Mark).AsEnumerable()
                .GroupBy(x => x.Name)
                .Select(x => x.First())
                .Select(Mapper.Map<CarMarkDto>);
        }

        public IEnumerable<WorkTypeDto> GetAvailableWorkTypesFromPool(string currentUserId)
        {
            return new List<WorkTypeDto>();
            /*UserManager.IsUserInCarServiceRole(currentUserId);
            var service = UnitOfWork.Repository<ICarServiceRepository>().GetByUserId(currentUserId);

            if (service.State == CarServiceState.Blocked)
            {
                return new List<WorkTypeDto>();
            }

            var query = UnitOfWork.Repository<IApplicationRepository>()
                .GetAll(true)
                .Include(app => app.WorkTypes)
                .Include(app => app.City)
                .Where(app => !app.IsDeleted && app.State == ApplicationState.InSearch);*/

            /*var result = query.Where(app => app.City.Id == service.City.Id && app.WorkTypes.Contains())
                .Select(x => x.WorkType)
                .ToList()
                .GroupBy(x => x.Name)
                .Select(x => x.First())
                .Select(x =>
                {
                    var dto = Mapper.Map<WorkTypeDto>(x);
                    dto.Name += $" ({x.Class.Name})";
                    return dto;
                })
                .ToList();

            return result;*/
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
                .Where(app => app.Car.User.ApplicationUser.Id == currentUserId);

            if (filter.State.HasValue)
            {
                if (filter.State.Value != ApplicationState.Deleted)
                {
                    query = query.Where(app => !app.IsDeleted);
                }
                query = query.Where(app => app.State == filter.State.Value);
            }
            else
            {
                query = query.Where(app => !app.IsDeleted);
            }

            if (filter.CreatedFrom.HasValue)
            {
                query = query.Where(app => app.Created >= filter.CreatedFrom.Value);
            }

            if (filter.CreatedTo.HasValue)
            {
                query = query.Where(app => app.Created <= filter.CreatedTo.Value);
            }

            if (filter.CarId.HasValue)
            {
                query = query.Where(app => app.Car.Id == filter.CarId.Value);
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

            if (filter.CreatedFrom.HasValue)
            {
                query = query.Where(app => app.Created >= filter.CreatedFrom.Value);
            }

            if (filter.CreatedTo.HasValue)
            {
                query = query.Where(app => app.Created <= filter.CreatedTo.Value);
            }

            return query.OrderByDescending(app => app.Created);
        }

        private IQueryable<Application> BuildQueryForPool(ApplicationsPoolFilter filter, CarService service)
        {
            if (service.State == CarServiceState.Blocked || service.State == CarServiceState.Rejected)
            {
                return new List<Application>().AsQueryable();
            }

            var query = UnitOfWork.Repository<IApplicationRepository>()
                .GetAll(true)
                .Include(app => app.WorkTypes)
                .Include(app => app.City)
                .Include(app => app.Car)
                .Include(app => app.Car.Model)
                .Include(app => app.Car.Model.Mark)
                .Where(app => !app.IsDeleted && app.State == ApplicationState.InSearch);

            query = query.Where(app => app.City.Id == service.City.Id);

            var workTypesId = service.WorkTypes.Select(s => s.Id).ToList();
            query = query.Where(app => app.WorkTypes.Any() && app.WorkTypes.All(p => workTypesId.Contains(p.Id)));

            if (filter.CreatedFrom.HasValue)
            {
                query = query.Where(app => app.Created >= filter.CreatedFrom.Value);
            }

            if (filter.CreatedTo.HasValue)
            {
                query = query.Where(app => app.Created <= filter.CreatedTo.Value);
            }

            if (filter.MarkId.HasValue)
            {
                query = query.Where(app => app.Car.Model.Mark.Id == filter.MarkId.Value);
            }

            return query.OrderByDescending(app => app.Created);
        }

        private IQueryable<Application> BuildQueryForAdministrator(ApplicationsFilter filter)
        {
            var query = UnitOfWork.Repository<IApplicationRepository>().GetAll(true);

            if (filter.State.HasValue)
            {
                if (filter.State.Value != ApplicationState.Deleted)
                {
                    query = query.Where(app => !app.IsDeleted);
                }
                query = query.Where(app => app.State == filter.State.Value);
            }
            else
            {
                query = query.Where(app => !app.IsDeleted);
            }

            if (filter.CreatedFrom.HasValue)
            {
                query = query.Where(app => app.Created >= filter.CreatedFrom.Value);
            }

            if (filter.CreatedTo.HasValue)
            {
                query = query.Where(app => app.Created <= filter.CreatedTo.Value);
            }

            return query.OrderByDescending(app => app.Created);
        }

        #endregion
    }
}
