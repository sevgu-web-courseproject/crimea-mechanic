using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using WebUI.Exception;
using WebUI.Models;
using WebUI.ProjectConstants;

namespace WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected string Token { get; set; }

        public string CurrentLangCode { get; protected set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }

            base.Initialize(requestContext);

            // Grab the user's login information from Identity
            var principal = User as ClaimsPrincipal;
            if (principal == null || !principal.Identity.IsAuthenticated || principal.Claims == null)
            {
                return;
            }
            var tokenClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimsConstants.ClaimToken);
            if (tokenClaim != null)
            {
                Token = tokenClaim.Value;
                ViewData["Token"] = tokenClaim.Value;
            }
        }

        protected void HandleBadRequest(ApiException apiException)
        {
            if (apiException.StatusCode == HttpStatusCode.BadRequest)
            {
                var badRequestData = JsonConvert.DeserializeObject<JsonBadRequest>(apiException.JsonData);

                if (badRequestData == null)
                {
                    ModelState.AddModelError("", HttpStatusCode.BadRequest.ToString());
                }
                else
                {

                    if (badRequestData.ModelState != null)
                    {
                        foreach (var modelStateItem in badRequestData.ModelState)
                        {
                            foreach (var message in modelStateItem.Value)
                            {
                                ModelState.AddModelError(modelStateItem.Key, message);
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(badRequestData.Message))
                    {
                        ModelState.AddModelError("", badRequestData.Message);
                    }

                    //When an error occurs while signing in, Error equals "invalid_grant" and ErrorDescription will contain more detail.
                    //This error is being set in the Web API project in the WebApi.Providers.ApplicationOAuthProvider class
                    if (string.Equals(badRequestData.Error, "Неверные права"))
                    {
                        ModelState.AddModelError("", badRequestData.ErrorDescription);
                    }
                }
            }
            else if (apiException.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Если пользователь не авторизован на WebApi - необходимо сделать LogOut
                var url = Url.Action("SignOut", "Account");
                if (url != null)
                {
                    Response.Redirect(url);
                }
            }
        }
    }
}