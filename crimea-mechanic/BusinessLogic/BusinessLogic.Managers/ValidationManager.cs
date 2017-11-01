using System;
using System.Text.RegularExpressions;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Validation;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class ValidationManager : BaseManager, IValidationManager
    {
        #region Constructor

        public ValidationManager(IUnitOfWork unitOfWork, IUserInternalManager userManager)
            : base(unitOfWork, userManager)
        {
        }

        #endregion

        #region Implementation of IValidationManager

        public ValidationResult ValidateRegistrationDto(RegistrationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.RegistrationDtoNotFound);
            }
            var validatioResult = new ValidationResult("Регистрация пользователя");
            var isRegular = dto is RegistrationUserDto;

            ValidateRegistartionBaseDto(dto, validatioResult);
            if (isRegular)
            {
                ValidateRegistrationUserDto((RegistrationUserDto) dto, validatioResult);
            }
            else
            {
                ValidateRegistrationCarServiceDto((RegistrationCarServiceDto) dto, validatioResult);
            }
            return validatioResult;
        }

        #endregion

        #region Private methods

        private void ValidateRegistartionBaseDto(RegistrationDto dto, ValidationResult validationResult)
        {
            var loginRegex = new Regex("^\\w[\\w-\\.]*@\\w+\\.\\w{2,4}$", RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(dto.Login) && !loginRegex.IsMatch(dto.Login))
            {
                validationResult.AddError(ValidationErrorResources.LoginIsNotValid);
            }
            if (!dto.Password.Equals(dto.PasswordСonfirmation))
            {
                validationResult.AddError(ValidationErrorResources.PasswordsNotMatch);
            }
        }

        private void ValidateRegistrationUserDto(RegistrationUserDto dto, ValidationResult validationResult)
        {
            if (string.IsNullOrEmpty(dto.ContactName))
            {
                validationResult.AddError(ValidationErrorResources.ContactNameIsEmpty);
            }
            var phoneRegex = new Regex("^\\+7(\\d{3}|\\(\\d{3}\\))\\-?\\d{3}\\-?\\d{2}\\-?\\d{2}$");
            if (!string.IsNullOrEmpty(dto.Phone) && !phoneRegex.IsMatch(dto.Phone))
            {
                validationResult.AddError(ValidationErrorResources.PhoneNumberIsIncorrect);
            }
        }

        private void ValidateRegistrationCarServiceDto(RegistrationCarServiceDto dto, ValidationResult validationResult)
        {

        }

        #endregion
    }
}
