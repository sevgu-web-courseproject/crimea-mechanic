using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebUI.Exception;
using WebUI.Helpers;
using WebUI.Models.User;
using WebUI.ProjectConstants;

namespace WebUI.Controllers
{
    public class AccountController : BaseController
    {
        #region Fields

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        #endregion

        #region Private methods

        private async Task IdentitySignin(SignInResult signInResult, bool isPersistent = false)
        {
            if (signInResult == null)
            {
                return;
            }
            string authenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInResult.UserName, ClaimsConstants.XmlSchemaString, authenticationType),
                new Claim(ClaimTypes.NameIdentifier, signInResult.UserId, ClaimsConstants.XmlSchemaString, authenticationType),
                new Claim(ClaimsConstants.ClaimToken, signInResult.AccessToken, ClaimsConstants.XmlSchemaString, authenticationType)
            };

            if (!string.IsNullOrEmpty(signInResult.DeclineReason))
            {
                claims.Add(new Claim(ClaimsConstants.ClaimDeclineReason, signInResult.DeclineReason, ClaimsConstants.XmlSchemaString, authenticationType));
            }

            if (!string.IsNullOrEmpty(signInResult.Roles))
            {
                claims.AddRange(signInResult.Roles.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(role => new Claim(ClaimTypes.Role, role, ClaimsConstants.XmlSchemaString, authenticationType)));
            }

            var identity = new ClaimsIdentity(claims, authenticationType);

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = signInResult.Expires,
                IssuedUtc = signInResult.Issued,
            }, identity);
        }

        private void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
        }

        #endregion Private methods

        #region Public methods

        // GET: Account/SignIn
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new SignInModel());
        }

        // POST: Account/SignIn
        [HttpPost]
        public async Task<ActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                SignInResult result = await WebApiService.Instance.AuthenticateAsync<SignInResult>(model.Login, model.Password);

                await IdentitySignin(result, model.RememberMe);

                return RedirectToAction("Index", "Home");
            }
            catch (ApiException ex)
            {
                //No 200 OK result, what went wrong?
                HandleBadRequest(ex);

                return View(model);
            }
        }

        // GET /Account/RegistrationUser
        [HttpGet]
        public ActionResult RegistrationUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET /Account/RegistrationCarService
        [HttpGet]
        public ActionResult RegistrationCarService()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyProfile()
        {
            if (User.IsInRole(Common.Constants.CommonRoles.Regular))
            {
                return View("ClientProfile");
            }
            if (User.IsInRole(Common.Constants.CommonRoles.CarService))
            {
                return View("CarServiceProfile");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        public ActionResult EditCarService()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> SignOut()
        {
            await WebApiService.Instance.LogOutAsync(Token);
            IdentitySignout();

            return RedirectToAction("SignIn", "Account");
        }

        #endregion
    }
}