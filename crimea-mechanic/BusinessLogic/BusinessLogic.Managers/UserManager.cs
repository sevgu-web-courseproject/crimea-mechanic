using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Constants;
using Common.Enums;
using Common.Exceptions;
using Common.Validation;
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
        private readonly IValidationManager _validationManager;

        #endregion

        #region Constructor

        public UserManager(IUserStore<IApplicationUser> store, IUnitOfWork unitOfWork, IValidationManager validationManager) : base(store)
        {
            _unitOfWork = unitOfWork;
            _validationManager = validationManager;

            UserValidator = new UserValidator<IApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true
            };

            var provider = new DpapiDataProtectionProvider("Crimea-Mechanic");
            UserTokenProvider = new DataProtectorTokenProvider<IApplicationUser, string>(provider.Create("UserToken"));
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

        public async Task RegistrationUser(RegistrationUserDto dto)
        {
            ValidationResult validationResult;
            try
            {
                validationResult = _validationManager.ValidateRegistrationUserDto(dto);
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessFaultException(ex.ParamName);
            }
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            var user = Mapper.Map<ApplicationUser>(dto);
            user.UserProfile = Mapper.Map<UserProfile>(dto);
            user.Id = Guid.NewGuid().ToString();
            user.EmailConfirmed = true;

            await CreateUser(user, dto.Password, CommonRoles.Regular);
        }

        public async Task RegistrationCarService(RegistrationCarServiceDto dto, string directory)
        {
            ValidationResult validationResult;
            try
            {
                validationResult = _validationManager.ValidateRegistrationCarServiceDto(dto);
            }
            catch (ArgumentNullException ex)
            {
                DeleteDirectoryWithFiles(directory);
                throw new BusinessFaultException(ex.ParamName);
            }
            if (validationResult.HasErrors)
            {
                DeleteDirectoryWithFiles(directory);
                throw new BusinessFaultException(validationResult.GetErrors());
            }
            var user = Mapper.Map<ApplicationUser>(dto);
            user.CarService = CreateCarService(dto);
            user.Id = Guid.NewGuid().ToString();
            user.EmailConfirmed = true;

            await CreateUser(user, dto.Password, CommonRoles.CarService);
        }

        public UserProfileDto GetUserProfile(string currentUserId)
        {
            IsUserInRegularRole(currentUserId);
            var profile = CheckAndGet(currentUserId).UserProfile;
            return Mapper.Map<UserProfileDto>(profile);
        }

        public void EditUserProfile(EditUserProfileDto dto, string currentUserId)
        {
            IsUserInRegularRole(currentUserId);
            var profile = CheckAndGet(currentUserId).UserProfile;

            var validationResult = _validationManager.ValidateEditUserProfileDto(dto);
            if (validationResult.HasErrors)
            {
                throw new BusinessFaultException(validationResult.GetErrors());
            }

            profile.Name = dto.Name;
            profile.Phone = dto.Phone;
            profile.Updated = DateTime.UtcNow;

            _unitOfWork.SaveChanges();
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

        public void IsUserInRegularRole(string userId)
        {
            if (!IsUserInRole(userId, CommonRoles.Regular))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserHasDifferentRole);
            }
        }

        public void IsUserInCarServiceRole(string userId)
        {
            if (!IsUserInRole(userId, CommonRoles.CarService))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserHasDifferentRole);
            }
        }

        public void IsUserInAdministrationRole(string userId)
        {
            if (!IsUserInRole(userId, CommonRoles.Administrator))
            {
                throw new BusinessFaultException(BusinessLogicExceptionResources.UserHasDifferentRole);
            }
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

        private void DeleteDirectoryWithFiles(string directory)
        {
            var filesDirInfo = new DirectoryInfo(directory);
            if (filesDirInfo.Exists)
            {
                filesDirInfo.Delete(true);
            }
        }

        private CarService CreateCarService(RegistrationCarServiceDto dto)
        {
            var carService = Mapper.Map<CarService>(dto);
            carService.City = _unitOfWork.Repository<ICityRepository>().Get(dto.CityId);

            carService.Phones = Mapper.Map<List<CarServicePhone>>(dto.Phones);

            if (dto.WorkTypes != null && dto.WorkTypes.Any())
            {
                var repository = _unitOfWork.Repository<IWorkTypeRepository>();
                carService.WorkTypes = new List<WorkType>();
                foreach (var workTypeId in dto.WorkTypes)
                {
                    carService.WorkTypes.Add(repository.Get(workTypeId));
                }
            }

            if (dto.CarTags != null && dto.CarTags.Any())
            {
                var repository = _unitOfWork.Repository<ICarMarksRepository>();
                carService.CarTags = new List<CarMark>();
                foreach (var carTagId in dto.CarTags)
                {
                    carService.CarTags.Add(repository.Get(carTagId));
                }
            }

            carService.Files = new List<CarServiceFile>();

            if (dto.Logo != null)
            {
                var logo = Mapper.Map<CarServiceFile>(dto.Logo);
                logo.Type = FileType.Logo;
                carService.Files.Add(logo);
            }

            if (dto.Photos != null && dto.Photos.Any())
            {
                ((List<CarServiceFile>)carService.Files).AddRange(Mapper.Map<List<CarServiceFile>>(dto.Photos));
            }

            return carService;
        }

        private async Task CreateUser(ApplicationUser user, string password, string role)
        {
            IdentityResult result;
            try
            {
                result = await CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw new BusinessFaultException(ex.Message);
            }
            if (!result.Succeeded)
            {
                throw new BusinessFaultException(string.Join(";", result.Errors.ToList()));
            }
            try
            {
                result = await AddToRoleAsync(user.Id, role);
            }
            catch (Exception ex)
            {
                throw new BusinessFaultException(ex.Message);
            }
            if (!result.Succeeded)
            {
                throw new BusinessFaultException(string.Join(";", result.Errors.ToList()));
            }
        }

        #endregion
    }
}
