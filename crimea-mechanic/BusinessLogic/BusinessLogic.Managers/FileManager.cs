using System.Linq;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Resources;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class FileManager : BaseManager, IFileManager
    {
        #region Constructor

        public FileManager(IUnitOfWork unitOfWork, IUserInternalManager userManager) : base(unitOfWork, userManager)
        {
        }

        #endregion

        #region Implemetation IFileManager

        public CarServiceFile GetCarServiceFile(long carServiceId, long fileId)
        {
            var service = UnitOfWork.Repository<ICarServiceRepository>().Get(carServiceId);
            if (service == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.CarServiceNotFound);
            }

            var file = service.Files.FirstOrDefault(f => !f.IsDeleted && f.Id == fileId);
            if (file == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.FileNotFound);
            }

            return file;

        }

        #endregion
    }
}
