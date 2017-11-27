using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Resources;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class FileManager : IFileManager
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public FileManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Implemetation IFileManager

        public CarServiceFile GetFile(long fileId)
        {
            var file = _unitOfWork.Repository<ICarServiceFileRepository>().Get(fileId);
            if (file == null || file.IsDeleted)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.FileNotFound);
            }

            return file;

        }

        #endregion
    }
}
