using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Works;
using BusinessLogic.Resources;
using Common.Exceptions;
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

        #endregion
    }
}
