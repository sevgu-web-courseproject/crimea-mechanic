using Microsoft.AspNet.Identity;

namespace DataAccessLayer.Models.Abstraction
{
    public interface IApplicationUser : IUser
    {
        string PasswordHash { get; set; }
    }
}
