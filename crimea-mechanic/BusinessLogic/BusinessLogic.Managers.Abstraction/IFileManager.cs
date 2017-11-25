using DataAccessLayer.Models;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IFileManager
    {
        CarServiceFile GetCarServiceFile(long carServiceId, long fileId);
    }
}
