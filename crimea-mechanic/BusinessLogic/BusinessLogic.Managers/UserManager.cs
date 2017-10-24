using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Managers.Abstraction;
using DataAccessLayer.Models.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;

namespace BusinessLogic.Managers
{
    public class UserManager : UserManager<IApplicationUser>, IUserManager
    {
        public UserManager(IUserStore<IApplicationUser> store) : base(store)
        {
            UserValidator = new UserValidator<IApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true
            };

            var provider = new DpapiDataProtectionProvider("Crimea-Mechanic");
            UserTokenProvider = new DataProtectorTokenProvider<IApplicationUser, string>(provider.Create("UserToken"));
        }

        /// <summary>
        /// Create Properties
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="isRegular">является ли пользователь с ролью Regular</param>
        /// <param name="isOperator">является ли пользователь с ролью оператора</param>
        /// <returns></returns>
        private AuthenticationProperties CreateProperties(IApplicationUser user, bool isRegular, bool isOperator)
        {

            var roles = new List<string>();
            if (isRegular)
            {
                roles.Add(CommonRoles.RegularRole);
            }
            if (isOperator)
            {
                roles.Add(CommonRoles.OperatorRole);
            }

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {ClaimsConstants.ClaimUserId, user.Id},
                {ClaimsConstants.ClaimUserName, user.UserName},
                {ClaimsConstants.ClaimUNeedToChangePassword, user.NeedToChangePassword.ToString()},
                {ClaimsConstants.ClaimRoles, string.Join(ClaimsConstants.ClaimRolesSeparator,roles.ToArray())}
            };
            return new AuthenticationProperties(data);
        }

        public async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = this as UserManager<IApplicationUser>;
            IApplicationUser user;
            try
            {
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch (Exception)
            {
                context.SetError(UserResources.InvalidGrant, UserResources.WrongUserNameOrPasswordError);
                return;
            }
            if (user == null)
            {
                context.SetError(UserResources.InvalidGrant, UserResources.WrongUserNameOrPasswordError);
                return;
            }
            if (!user.EmailConfirmed)
            {
                context.SetError(UserResources.InvalidGrant, UserResources.UserEmailIsNotConfirmedError);
                return;
            }
            var oAuthIdentity = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            var isRegular = await userManager.IsInRoleAsync(user.Id, CommonRoles.RegularRole);
            var isOperator = await userManager.IsInRoleAsync(user.Id, CommonRoles.OperatorRole);
            var properties = CreateProperties(user, isRegular, isOperator);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            var cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }
    }
}
