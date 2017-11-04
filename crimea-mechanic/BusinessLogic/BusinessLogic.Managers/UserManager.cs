using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Constants;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Abstraction;
using DataAccessLayer.Repositories.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;

namespace BusinessLogic.Managers
{
    public class UserManager : UserManager<IApplicationUser>, IUserManager, IUserInternalManager
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        //private readonly IValidationManager _validationManager;

        #endregion

        #region Constructor

        public UserManager(IUserStore<IApplicationUser> store, IUnitOfWork unitOfWork) : base(store)
        {
            _unitOfWork = unitOfWork;
            //_validationManager = validationManager;

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

        #endregion

        #region Private Methods

        /// <summary>
        /// Create Properties
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private AuthenticationProperties CreateProperties(IApplicationUser user, IList<string> roles)
        {

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {ClaimsConstants.ClaimUserId, user.Id},
                {ClaimsConstants.ClaimUserName, user.UserName},
                {ClaimsConstants.ClaimRole, string.Join("|", roles)}
            };
            return new AuthenticationProperties(data);
        }

        #endregion

        #region Implementation of IUserManager

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
                context.SetError("Неверные права", "Неверный логин или пароль");
                return;
            }
            if (user == null)
            {
                context.SetError("Неверные права", "Неверный логин или пароль");
                return;
            }
            var oAuthIdentity = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            var roles = userManager.GetRoles(user.Id);
            var properties = CreateProperties(user, roles);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            var cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public void RegistrationUser(RegistrationUserDto dto)
        {
            throw new NotImplementedException();
        }

        public void RegistrationCarService(RegistrationCarServiceDto dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IUserInternalManager

        public ApplicationUser CheckAndGet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserNotFound);
            }
            var user = _unitOfWork.Repository<IApplicationUserRepository>().Get(userId);
            if (user == null)
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserNotFound);
            }
            return user;
        }

        public bool IsUserInRole(string userId, string role)
        {
            return this.IsInRole(userId, role);
        }

        #endregion
    }
}
