using DataAccessLayer.Models;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IUserInternalManager
    {
        ApplicationUser CheckAndGet(string userId);

        bool IsUserInRole(string userId, string role);
    }
}
