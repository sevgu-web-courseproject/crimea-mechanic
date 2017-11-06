using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace WebApi.Controllers
{
    [Authorize]
    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET api/Account/Logout
        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/RegistrationUser
        [HttpPost]
        [Route("RegistrationUser")]
        [AllowAnonymous]
        public Task<IHttpActionResult> RegistrationUser(RegistrationUserDto dto)
        {
            return CallBusinessLogicActionAsync(() => _userManager.RegistrationUser(dto));
        }

        // POST api/Account/RegistrationCarService
        [HttpPost]
        [Route("RegistrationCarService")]
        [AllowAnonymous]
        public Task<IHttpActionResult> RegistrationCarService()
        {
            var directory = Guid.NewGuid().ToString("N");
            var dto = GetRegistrationServiceDto(directory);
            return CallBusinessLogicActionAsync(() => _userManager.RegistrationCarService(dto, directory));
        }

        #region Private methods

        private RegistrationCarServiceDto GetRegistrationServiceDto(string directory)
        {
            return new RegistrationCarServiceDto
            {
                Login = GetStringFromRequest("Login"),
                Password = GetStringFromRequest("Password"),
                PasswordСonfirmation = GetStringFromRequest("PasswordConfirmation"),
                Name = GetStringFromRequest("Name"),
                Address = GetStringFromRequest("Address"),
                Email = GetStringFromRequest("Email"),
                Phones = GetList<string>("Phones"),
                ManagerName = GetStringFromRequest("ManagerName"),
                Site = GetStringFromRequest("Site"),
                TimetableWorks = GetStringFromRequest("TimetableWorks"),
                WorkTags = GetList<long>("WorkTags"),
                CarTags = GetList<long>("CarTags"),
                About = GetStringFromRequest("About"),
                Logo = SaveFile("Logo", directory),
                Photos = SaveFiles("Photos", directory)
            };
        }

        #endregion
    }
}
