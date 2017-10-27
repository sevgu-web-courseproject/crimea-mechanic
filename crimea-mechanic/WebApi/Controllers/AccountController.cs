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

        // Get api/Account/Logout
        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public Task<IHttpActionResult> Register(RegisterUserDto dto)
        {
            return CallBusinessLogicActionAsync(() => _userManager.Register(dto));
        }
    }
}
