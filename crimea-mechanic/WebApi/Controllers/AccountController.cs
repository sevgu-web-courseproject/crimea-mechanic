using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace WebApi.Controllers
{
    [Authorize]
    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        // Get api/Account/Logout
        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // Get api/Account/Register
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public Task<IHttpActionResult> Register(RegisterUserDto dto)
        {
            dto.RemoteIp = HttpContext.Current.Request.UserHostAddress;
            return CallBusinessLogicActionAsync(_ => _userManager.Register(dto));
        }
    }
}
