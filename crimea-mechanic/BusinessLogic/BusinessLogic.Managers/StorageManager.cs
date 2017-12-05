using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Storage;
using BusinessLogic.Objects.Works;
using BusinessLogic.Resources;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    /// <summary>
    /// Менеджер для работы со статичными данными
    /// </summary>
    public class StorageManager : BaseManager, IStorageManager
    {
        #region Constructor

        public StorageManager(IUnitOfWork unitOfWork, IUserInternalManager userManager) : base(unitOfWork, userManager)
        {
            
        }

        #endregion

        #region Implementation of StorageManager

        public IEnumerable<CarMarkDto> GetCarMarks(string searchText)
        {
            var query = UnitOfWork.Repository<ICarMarksRepository>().GetAll(true)
                .Where(mark => !mark.IsDeleted);
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(mark => mark.Name.Contains(searchText));
            }
            return query.ToList().Select(Mapper.Map<CarMarkDto>);
        }

        public IEnumerable<CarModelDto> GetModels(long markId)
        {
            var mark = UnitOfWork.Repository<ICarMarksRepository>().GetAll(true)
                .Include(m => m.Models)
                .FirstOrDefault(m => !m.IsDeleted && m.Id == markId);
            if (mark == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarMarkNotFound);
            }
            var models = mark.Models.Where(model => !model.IsDeleted);
            return Mapper.Map<IEnumerable<CarModelDto>>(models);
        }

        public IEnumerable<CityDto> GetCities()
        {
            return UnitOfWork.Repository<ICityRepository>().GetAll(true)
                .Where(city => !city.IsDeleted)
                .ToList()
                .Select(Mapper.Map<CityDto>);
        }

        public IEnumerable<WorkClassDto> GetWorkClasses()
        {
            return UnitOfWork.Repository<IWorkClassRepository>()
                .GetAll(true)
                .Include(p => p.WorkTypes)
                .Where(p => !p.IsDeleted)
                .ToList()
                .Select(workClass =>
                {
                    var dto = Mapper.Map<WorkClassDto>(workClass);
                    dto.Types = workClass.WorkTypes
                        .Where(p => !p.IsDeleted)
                        .Select(Mapper.Map<WorkTypeDto>);
                    return dto;
                });
        }

        public void AddCity(AddCityDto dto, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<ICityRepository>();
            var city = repository.GetAll(true).SingleOrDefault(c => !c.IsDeleted && c.Name == dto.Name);
            if (city != null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CityAlreadyContains);
            }
            city = Mapper.Map<City>(dto);
            repository.Add(city);
            UnitOfWork.SaveChanges();
        }

        public void AddCarMark(AddCarMarkDto dto, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<ICarMarksRepository>();
            var mark = repository.GetAll(true).SingleOrDefault(m => !m.IsDeleted && m.Name == dto.Name);
            if (mark != null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarMarkAlreadyContains);
            }
            mark = Mapper.Map<CarMark>(dto);
            repository.Add(mark);
            UnitOfWork.SaveChanges();
        }

        public void AddCarModel(AddCarModelDto dto, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<ICarMarksRepository>();
            var mark = repository.Get(dto.MardId);
            if (mark == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarMarkNotFound);
            }
            var model = mark.Models.SingleOrDefault(m => !m.IsDeleted && m.Name == dto.Name);
            if (model != null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarModelAlreadyContains);
            }
            model = Mapper.Map<CarModel>(dto);
            mark.Models.Add(model);
            repository.Update(mark);
            UnitOfWork.SaveChanges();
        }

        public void AddWorkClass(AddWorkClassDto dto, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<IWorkClassRepository>();
            var workClass = repository.GetAll(true).SingleOrDefault(w => !w.IsDeleted && w.Name == dto.Name);
            if (workClass != null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.WorkClassAlreadyContains);
            }
            workClass = Mapper.Map<WorkClass>(dto);
            repository.Add(workClass);
            UnitOfWork.SaveChanges();
        }

        public void AddWorkType(AddWorkTypeDto dto, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<IWorkClassRepository>();
            var workClass = repository.Get(dto.WorkClassId);
            if (workClass == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.WorkClassNotFound);
            }
            var workType = workClass.WorkTypes.SingleOrDefault(w => !w.IsDeleted && w.Name == dto.Name);
            if (workType != null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.WorkTypeAlreadyContains);
            }
            workType = Mapper.Map<WorkType>(dto);
            workClass.WorkTypes.Add(workType);
            repository.Update(workClass);
            UnitOfWork.SaveChanges();
        }

        public void DeleteCity(long cityId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<ICityRepository>();
            var city = repository.Get(cityId);
            if (city == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CityNotFound);
            }
            city.IsDeleted = true;
            city.Updated = DateTime.UtcNow;
            repository.Update(city);
            UnitOfWork.SaveChanges();
        }

        public void DeleteMark(long markId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<ICarMarksRepository>();
            var mark = repository.Get(markId);
            if (mark == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarMarkNotFound);
            }
            mark.IsDeleted = true;
            mark.Updated = DateTime.UtcNow;
            repository.Update(mark);
            UnitOfWork.SaveChanges();
        }

        public void DeleteModel(long modelId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<ICarModelsRepository>();
            var model = repository.Get(modelId);
            if (model == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarModelNotFound);
            }
            model.IsDeleted = true;
            model.Updated = DateTime.UtcNow;
            repository.Update(model);
            UnitOfWork.SaveChanges();
        }

        public void DeleteWorkClass(long workClassId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<IWorkClassRepository>();
            var workClass = repository.Get(workClassId);
            if (workClass == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.WorkClassNotFound);
            }
            workClass.IsDeleted = true;
            workClass.Updated = DateTime.UtcNow;
            repository.Update(workClass);
            UnitOfWork.SaveChanges();
        }

        public void DeleteWorkType(long workTypeId, string currentUserId)
        {
            UserManager.IsUserInAdministrationRole(currentUserId);
            var repository = UnitOfWork.Repository<IWorkTypeRepository>();
            var workType = repository.Get(workTypeId);
            if (workType == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.WorkTypeNotFound);
            }
            workType.IsDeleted = true;
            workType.Updated = DateTime.UtcNow;
            repository.Update(workType);
            UnitOfWork.SaveChanges();
        }

        #endregion
    }
}
