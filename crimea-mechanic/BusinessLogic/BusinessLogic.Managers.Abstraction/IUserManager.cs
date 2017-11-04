using System.Threading.Tasks;
using BusinessLogic.Objects.User;
using Microsoft.Owin.Security.OAuth;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IUserManager
    {
        Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context);

        void RegistrationUser(RegistrationUserDto dto);

        void RegistrationCarService(RegistrationCarServiceDto dto);
    }
}
