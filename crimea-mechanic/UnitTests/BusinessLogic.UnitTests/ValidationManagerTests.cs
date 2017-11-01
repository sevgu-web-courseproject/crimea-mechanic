using System;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Enums;
using Common.Validation;
using DataAccessLayer.Repositories.Abstraction;
using Moq;
using NUnit.Framework;

namespace BusinessLogic.UnitTests
{
    [TestFixture(Description = "Юнит-тесты для менеджера валидации")]
    public class ValidationManagerTests
    {
        #region Fields

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserInternalManager> _userManager;
        private ValidationManager _manager;

        #endregion

        #region Init

        [OneTimeSetUp]
        public void ConfigureSet()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userManager = new Mock<IUserInternalManager>();
            _manager = new ValidationManager(_unitOfWorkMock.Object, _userManager.Object);
        }

        [SetUp]
        public void Init()
        {
            _unitOfWorkMock.Reset();
            _userManager.Reset();
        }

        #endregion

        #region ValidateRegistrationDto

        [Test(Description = "ValidateRegistrationDto должен кинуть ошибку если не передана дто")]
        public void ValidateRegistrationDto_Must_Throw_Exception_When_Dto_Not_Found()
        {
            //Arrange
            //Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _manager.ValidateRegistrationDto(null));
            Assert.AreEqual(ArgumentExceptionResources.RegistrationDtoNotFound, exception.ParamName);
        }

        [Test(Description = "ValidateRegistrationDto должен провалидировать дто регистрации обычного пользователя без ошибок")]
        public void ValidateRegistrationDto_Must_Validate_Registration_User_Dto_Without_Errors()
        {
            //Arrange
            var dto = new RegistrationUserDto
            {
                ContactName = "Test",
                Login = "test@test.com",
                Password = "qwerty",
                PasswordСonfirmation = "qwerty",
                Phone = "+7(111)-111-11-11",
                Type = RegistrationTypeEnum.Regular
            };

            //Act
            ValidationResult result = null;
            Assert.DoesNotThrow(() => result = _manager.ValidateRegistrationDto(dto));

            //Assert
            Assert.False(result.HasErrors);
        }

        [Test(Description = "ValidateRegistrationDto должен провалидировать дто регистрации обычного пользователя и вернуть все возможные ошибки")]
        public void ValidateRegistrationDto_Must_Validate_Registration_User_Dto_With_All_Possible_Errors()
        {
            //Arrange
            var dto = new RegistrationUserDto
            {
                ContactName = "",
                Login = "test@@test.com",
                Password = "qwerty",
                PasswordСonfirmation = "qwerty1",
                Phone = "+7(111)-111-11-11312",
                Type = RegistrationTypeEnum.Regular
            };

            var exceptions = string.Join(";",
                ValidationErrorResources.LoginIsNotValid,
                ValidationErrorResources.PasswordsNotMatch,
                ValidationErrorResources.ContactNameIsEmpty,
                ValidationErrorResources.PhoneNumberIsIncorrect);
            var exceptionMessage = $"Регистрация пользователя: {exceptions}";

            //Act
            ValidationResult result = null;
            Assert.DoesNotThrow(() => result = _manager.ValidateRegistrationDto(dto));

            //Assert
            Assert.True(result.HasErrors);
            Assert.AreEqual(exceptionMessage, result.GetErrors());
        }

        #endregion
    }
}
