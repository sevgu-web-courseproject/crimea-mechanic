using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
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

        [HttpGet]
        [Route("Image")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Image()
        {
            var path = HttpContext.Current.Server.MapPath("~/Files/test.png");
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var streamContent = new StreamContent(stream);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync())
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "test.png"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                return result;
            }
        }
    }
}
