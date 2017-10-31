using DataAccessLayer.Models;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IUserInternalManager
    {
        ApplicationUser CheckAndGet(string userId);

        bool IsInRole(string userId, string role);
    }
}
