using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Validation;
using DataAccessLayer.Repositories.Abstraction;

namespace BusinessLogic.Managers
{
    public class ValidationManager : IValidationManager
    {

        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public ValidationManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Implementation of IValidationManager

        public ValidationResult ValidateRegistrationUserDto(RegistrationUserDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.RegistrationDtoNotFound);
            }
            var validationResult = new ValidationResult("Регистрация пользователя");

            ValidateRegistartionBaseDto(dto, validationResult);

            if (string.IsNullOrEmpty(dto.ContactName))
            {
                validationResult.AddError(ValidationErrorResources.ContactNameIsEmpty);
            }
            if (IsInvalidPhoneNumber(dto.Phone))
            {
                validationResult.AddError(ValidationErrorResources.PhoneNumberIsIncorrect);
            }

            return validationResult;
        }

        public ValidationResult ValidateRegistrationCarServiceDto(RegistrationCarServiceDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.RegistrationDtoNotFound);
            }
            var validationResult = new ValidationResult("Регистрация автосервиса");

            ValidateRegistartionBaseDto(dto, validationResult);

            if (string.IsNullOrEmpty(dto.Name))
            {
                validationResult.AddError(ValidationErrorResources.CarServiceNameIsEmpty);
            }

            if (string.IsNullOrEmpty(dto.Address))
            {
                validationResult.AddError(ValidationErrorResources.CarServiceAddressIsEmpty);
            }

            if (IsInvalidEmail(dto.Email))
            {
                validationResult.AddError(ValidationErrorResources.CarServiceContactEmailIsInvalid);
            }

            if (dto.Phones == null || !dto.Phones.Any())
            {
                validationResult.AddError(ValidationErrorResources.CarServicePhonesIsEmpty);
            }
            if (dto.Phones != null)
            {
                foreach (var phone in dto.Phones)
                {
                    if (IsInvalidPhoneNumber(phone))
                    {
                        validationResult.AddError(string.Format(ValidationErrorResources.CarServicePhoneIsIncorrect, phone));
                    }
                }
            }

            if (string.IsNullOrEmpty(dto.ManagerName))
            {
                validationResult.AddError(ValidationErrorResources.CarServiceManagerNameIsEmpty);
            }

            var regex = new Regex("^(http|https)://");
            if (string.IsNullOrEmpty(dto.Site) || !regex.IsMatch(dto.Site))
            {
                validationResult.AddError(ValidationErrorResources.CarServiceSiteIsIncorrect);
            }

            if (dto.CarTags != null && dto.CarTags.Any())
            {
                var repository = _unitOfWork.Repository<ICarMarksRepository>();
                foreach (var carTagId in dto.CarTags)
                {
                    var mark = repository.Get(carTagId);
                    if (mark == null)
                    {
                        validationResult.AddError(string.Format(ValidationErrorResources.CarMarkNotFound, carTagId));
                    }
                }
            }

            if (dto.WorkTags != null && dto.WorkTags.Any())
            {
                var repository = _unitOfWork.Repository<IWorkTagsRepository>();
                foreach (var workTagId in dto.WorkTags)
                {
                    var workTag = repository.Get(workTagId);
                    if (workTag == null)
                    {
                        validationResult.AddError(string.Format(ValidationErrorResources.WorkTagNotFound, workTagId));
                    }
                }
            }

            if (dto.Logo != null && !IsImage(dto.Logo.Name))
            {
                validationResult.AddError(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Logo.Name));
            }

            if (dto.Photos != null && dto.Photos.Any())
            {
                foreach (var photo in dto.Photos)
                {
                    if (!IsImage(photo.Name))
                    {
                        validationResult.AddError(string.Format(ValidationErrorResources.InvalidFileExtension, photo.Name));
                    }
                }
            }

            return validationResult;
        }

        #endregion

        #region Private methods

        private void ValidateRegistartionBaseDto(RegistrationDto dto, ValidationResult validationResult)
        {
            if (IsInvalidEmail(dto.Login))
            {
                validationResult.AddError(ValidationErrorResources.LoginIsNotValid);
            }
            if (!dto.Password.Equals(dto.PasswordСonfirmation))
            {
                validationResult.AddError(ValidationErrorResources.PasswordsNotMatch);
            }
        }

        private bool IsInvalidEmail(string email)
        {
            var regex = new Regex("^\\w[\\w-\\.]*@\\w+\\.\\w{2,4}$", RegexOptions.IgnoreCase);
            return string.IsNullOrEmpty(email) || !regex.IsMatch(email);
        }

        private bool IsInvalidPhoneNumber(string phoneNumber)
        {
            var regex = new Regex("^\\+7(\\d{3}|\\(\\d{3}\\))\\-?\\d{3}\\-?\\d{2}\\-?\\d{2}$");
            return string.IsNullOrEmpty(phoneNumber) || !regex.IsMatch(phoneNumber);
        }

        private bool IsImage(string fileName)
        {
            var validExtensions = new List<string>
            {
                ".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".gif"
            };
            var extension = Path.GetExtension(fileName);
            return validExtensions.Contains(extension);
        }
            
        #endregion
    }
}
