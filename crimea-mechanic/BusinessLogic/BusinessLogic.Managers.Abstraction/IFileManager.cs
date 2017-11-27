using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IFileManager
    {
        CarServiceFile GetFile(long fileId);
    }
}
