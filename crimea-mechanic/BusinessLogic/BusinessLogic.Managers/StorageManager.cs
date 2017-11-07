using System.Collections.Generic;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
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

        public List<CarMarkDto> GetCarMarks(string searchText)
        {
            throw new System.NotImplementedException();
        }

        public List<CarModelDto> GetModels(long markId)
        {
            throw new System.NotImplementedException();
        }

        public List<WorkTagDto> GetWorkTags()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
