﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Application;
using BusinessLogic.Objects.Car;
using BusinessLogic.Objects.CarService;
using BusinessLogic.Objects.Review;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Enums;
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

            var city = _unitOfWork.Repository<ICityRepository>().Get(dto.CityId);
            if (city == null)
            {
                validationResult.AddError(ValidationErrorResources.CityNotFound);
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

            if (dto.WorkTypes != null && dto.WorkTypes.Any())
            {
                var repository = _unitOfWork.Repository<IWorkTypeRepository>();
                foreach (var workType in dto.WorkTypes)
                {
                    var type = repository.Get(workType);
                    if (type == null)
                    {
                        validationResult.AddError(string.Format(ValidationErrorResources.WorkTypeNotFound, workType));
                    }
                }
            }
            else
            {
                validationResult.AddError(ValidationErrorResources.WorkTypeRequired);
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

            if (dto.Logo != null && !IsImage(dto.Logo.Name))
            {
                validationResult.AddError(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Logo.Name));
            }

            if (dto.Photos != null && dto.Photos.Any())
            {
                if (dto.Photos.Count > 5)
                {
                    validationResult.AddError(ValidationErrorResources.PhotosToMuch);
                }
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

        public ValidationResult ValidateUserCarDto(AddOrEditUserCarDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.AddUserCarDtoNotFound);
            }
            ValidationResult validationResult;
            if (!dto.Id.HasValue)
            {
                validationResult = new ValidationResult("Добавление машины пользователя");
                ValidateAddUserCarDto(dto, validationResult);
            }
            else
            {
                validationResult = new ValidationResult("Редактирование машины пользователя");
                ValidateEditUserCarDto(dto, validationResult);
            }
            return validationResult;
        }

        public ValidationResult ValidateCreateApplicationDto(CreateApplicationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.CreateApplicationDtoNotFound);
            }
            var validationResult = new ValidationResult("Создание новой заявки");

            var car = _unitOfWork.Repository<IUserCarRepository>().Get(dto.CarId);
            if (car == null)
            {
                validationResult.AddError(ValidationErrorResources.UserCarNotFound);
            }

            var city = _unitOfWork.Repository<ICityRepository>().Get(dto.CityId);
            if (city == null)
            {
                validationResult.AddError(ValidationErrorResources.CityNotFound);
            }

            if (string.IsNullOrEmpty(dto.Description))
            {
                validationResult.AddError(ValidationErrorResources.ApplicationDescriptionIsEmpty);
            }

            if (dto.WorkTypes != null && dto.WorkTypes.Any())
            {
                var repository = _unitOfWork.Repository<IWorkTypeRepository>();
                foreach (var workTypeId in dto.WorkTypes)
                {
                    var workType = repository.Get(workTypeId);
                    if (workType == null)
                    {
                        validationResult.AddError(string.Format(ValidationErrorResources.WorkTypeNotFound, workType));
                    }
                }
            }

            return validationResult;
        }

        public ValidationResult ValidateEditApplicationDto(EditApplicationDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.EditApplicationDtoNotFound);
            }
            var validationResult = new ValidationResult("Редактирование заявки");

            var application = _unitOfWork.Repository<IApplicationRepository>().Get(dto.ApplicationId);
            if (application == null)
            {
                validationResult.AddError(ValidationErrorResources.ApplicationNotFound);
            }

            if (string.IsNullOrEmpty(dto.Description))
            {
                validationResult.AddError(ValidationErrorResources.ApplicationDescriptionIsEmpty);
            }

            return validationResult;
        }

        public ValidationResult ValidateAddOfferDto(AddOfferDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(ArgumentExceptionResources.AddOfferDtoNotFound);
            }
            var validationResult = new ValidationResult("Добавление предложения");

            var application = _unitOfWork.Repository<IApplicationRepository>().Get(dto.ApplicationId);
            if (application == null)
            {
                validationResult.AddError(ValidationErrorResources.ApplicationNotFound);
            }

            if (dto.Price == default(float))
            {
                validationResult.AddError(ValidationErrorResources.OfferPriceNotFound);
            }

            if (string.IsNullOrEmpty(dto.Content))
            {
                validationResult.AddError(ValidationErrorResources.OfferContentNotFound);
            }

            return validationResult;
        }

        public ValidationResult ValidateEditCarServiceDto(EditCarServiceDto dto)
        {
            var validationResult = new ValidationResult("Редактирование автосервиса");

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

            if (dto.Logo != null && !IsImage(dto.Logo.Name))
            {
                validationResult.AddError(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Logo.Name));
            }

            if (dto.Photos != null && dto.Photos.Any())
            {
                if (dto.Photos.Count > 5)
                {
                    validationResult.AddError(ValidationErrorResources.PhotosToMuch);
                }
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

        public ValidationResult ValidateOperateReviewDto(OperateReviewDto dto)
        {
            var validationResult = new ValidationResult("Добавление/Редактирование отзыва");
            if (dto.Mark == 0)
            {
                validationResult.AddError(ValidationErrorResources.ReviewMarkIsEmpty);
            }
            if (string.IsNullOrEmpty(dto.Review))
            {
                validationResult.AddError(ValidationErrorResources.ReviewContentIsEmpty);
            }
            return validationResult;
        }

        public ValidationResult ValidateEditUserProfileDto(EditUserProfileDto dto)
        {
            var validationResult = new ValidationResult("Редиктирование профайла пользователя");
            if (string.IsNullOrEmpty(dto.Name))
            {
                validationResult.AddError(ValidationErrorResources.ContactNameIsEmpty);
            }
            if (string.IsNullOrEmpty(dto.Phone) || IsInvalidPhoneNumber(dto.Phone))
            {
                validationResult.AddError(ValidationErrorResources.PhoneNumberIsIncorrect);
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
            var regex = new Regex("^\\+7(\\d{3}|\\(\\d{3}\\)) ?\\d{3}\\-?\\d{2}\\-?\\d{2}$");
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

        private void ValidateCommonFieldsForAddOrEditUserDto(AddOrEditUserCarDto dto, ValidationResult validationResult)
        {
            if (dto.FuelType == FuelType.Unknown)
            {
                validationResult.AddError(ValidationErrorResources.FuelTypeNotFound);
            }
            if (dto.Vin.Length != 17)
            {
                validationResult.AddError(ValidationErrorResources.VinNumberIsIncorrect);
            }
        }

        private void ValidateAddUserCarDto(AddOrEditUserCarDto dto, ValidationResult validationResult)
        {
            if (dto.ModelId.HasValue)
            {
                var model = _unitOfWork.Repository<ICarModelsRepository>().Get(dto.ModelId.Value);
                if (model == null)
                {
                    validationResult.AddError(ValidationErrorResources.CarModelNotFound);
                }
            }
            else
            {
                validationResult.AddError(ValidationErrorResources.CarModelNotFound);
            }
            ValidateCommonFieldsForAddOrEditUserDto(dto, validationResult);
        }

        private void ValidateEditUserCarDto(AddOrEditUserCarDto dto, ValidationResult validationResult)
        {
            if (dto.Id.HasValue)
            {
                var car = _unitOfWork.Repository<IUserCarRepository>().Get(dto.Id.Value);
                if (car == null)
                {
                    validationResult.AddError(ValidationErrorResources.UserCarNotFound);
                }
            }
            else
            {
                validationResult.AddError(ValidationErrorResources.UserCarNotFound);
            }
            ValidateCommonFieldsForAddOrEditUserDto(dto, validationResult);
        }
            
        #endregion
    }
}
