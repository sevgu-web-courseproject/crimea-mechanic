using System;
using System.Collections.Generic;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Objects.User;
using BusinessLogic.Resources;
using Common.Validation;
using DataAccessLayer.Models;
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
        private ValidationManager _manager;

        #endregion

        #region Init

        [OneTimeSetUp]
        public void ConfigureSet()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _manager = new ValidationManager(_unitOfWorkMock.Object);
        }

        [SetUp]
        public void Init()
        {
            _unitOfWorkMock.Reset();
        }

        #endregion

        #region ValidateRegistrationUserDto

        [Test(Description = "ValidateRegistrationUserDto должен кинуть ошибку если не передана дто")]
        public void ValidateRegistrationUserDto_Must_Throw_Exception_When_Dto_Not_Found()
        {
            //Arrange
            //Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _manager.ValidateRegistrationUserDto(null));
            Assert.AreEqual(ArgumentExceptionResources.RegistrationDtoNotFound, exception.ParamName);
        }

        [Test(Description = "ValidateRegistrationUserDto должен провалидировать дто регистрации обычного пользователя без ошибок")]
        public void ValidateRegistrationUserDto_Must_Validate_Registration_User_Dto_Without_Errors()
        {
            //Arrange
            var dto = new RegistrationUserDto
            {
                ContactName = "Test",
                Login = "test@test.com",
                Password = "qwerty",
                PasswordСonfirmation = "qwerty",
                Phone = "+7(111)-111-11-11",
            };

            //Act
            ValidationResult result = null;
            Assert.DoesNotThrow(() => result = _manager.ValidateRegistrationUserDto(dto));

            //Assert
            Assert.False(result.HasErrors);
        }

        [Test(Description = "ValidateRegistrationUserDto должен провалидировать дто регистрации обычного пользователя и вернуть все возможные ошибки")]
        public void ValidateRegistrationUserDto_Must_Validate_Registration_User_Dto_With_All_Possible_Errors()
        {
            //Arrange
            var dto = new RegistrationUserDto
            {
                ContactName = "",
                Login = "test@@test.com",
                Password = "qwerty",
                PasswordСonfirmation = "qwerty1",
                Phone = "+7(111)-111-11-11312",
            };

            //Act
            ValidationResult result = null;
            Assert.DoesNotThrow(() => result = _manager.ValidateRegistrationUserDto(dto));

            //Assert
            Assert.True(result.HasErrors);
            Assert.AreEqual(ValidationErrorResources.LoginIsNotValid, result.Errors[0]);
            Assert.AreEqual(ValidationErrorResources.PasswordsNotMatch, result.Errors[1]);
            Assert.AreEqual(ValidationErrorResources.ContactNameIsEmpty, result.Errors[2]);
            Assert.AreEqual(ValidationErrorResources.PhoneNumberIsIncorrect, result.Errors[3]);
        }

        #endregion

        #region ValidateRegistrationCarServiceDto

        [Test(Description = "ValidateRegistrationCarServiceDto должен кинуть ошибку если не передана дто")]
        public void ValidateRegistrationCarServiceDto_Must_Throw_Exception_When_Dto_Not_Found()
        {
            //Arrange
            //Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _manager.ValidateRegistrationCarServiceDto(null));
            Assert.AreEqual(ArgumentExceptionResources.RegistrationDtoNotFound, exception.ParamName);
        }

        [Test(Description = "ValidateRegistrationCarServiceDto должен провалидировать дто регистрации автосервиса без ошибок")]
        public void ValidateRegistrationCarServiceDto_Must_Validate_Registration_CarService_Dto_Without_Errors()
        {
            //Arrange
            var dto = new RegistrationCarServiceDto
            {
                Login = "test@test.com",
                Password = "qwerty",
                PasswordСonfirmation = "qwerty",
                Name = "Test",
                Address = "Test",
                Email = "test@test.com",
                Phones = new List<string>
                {
                    "+79785125532", "+79784216546"
                },
                ManagerName = "Test",
                Site = "https://test.com",
                TimetableWorks = "Test",
                WorkTags = new List<long>
                {
                    1, 2
                },
                CarTags = new List<long>
                {
                    1, 2
                },
                Logo = new FileDto
                {
                    Name = "Test.png",
                    Path = "Test"
                },
                Photos = new List<FileDto>
                {
                    new FileDto
                    {
                        Name = "test.png",
                        Path = "test"
                    },
                    new FileDto
                    {
                        Name = "test.png",
                        Path = "test"
                    }
                },
                About = "test"
            };

            var workTagsRepository = new Mock<IWorkTagsRepository>();
            workTagsRepository.Setup(act => act.Get(It.IsAny<long>())).Returns(new WorkTag());
            _unitOfWorkMock.Setup(act => act.Repository<IWorkTagsRepository>()).Returns(workTagsRepository.Object);

            var marksRepository = new Mock<ICarMarksRepository>();
            marksRepository.Setup(act => act.Get(It.IsAny<long>())).Returns(new CarMark());
            _unitOfWorkMock.Setup(act => act.Repository<ICarMarksRepository>()).Returns(marksRepository.Object);

            //Act
            ValidationResult result = null;
            Assert.DoesNotThrow(() => result = _manager.ValidateRegistrationCarServiceDto(dto));

            //Assert
            Assert.False(result.HasErrors);
        }

        [Test(Description = "ValidateRegistrationCarServiceDto должен провалидировать дто регистрации автосервиса и бросить все возможные ошибки")]
        [TestCase(true)]
        [TestCase(false)]
        public void ValidateRegistrationCarServiceDto_Must_Validate_Registration_CarService_Dto_With_All_Possible_Errors(bool withPhones)
        {
            //Arrange
            var phone1 = "+79785166623125532";
            var phone2 = "79784124216546";
            var dto = new RegistrationCarServiceDto
            {
                Login = "test@@test.com",
                Password = "qwerty",
                PasswordСonfirmation = "qwerty1",
                Name = null,
                Address = "",
                Email = "test@@test.com",
                Phones = withPhones 
                    ? new List<string> { phone1, phone2 } 
                    : new List<string>(),
                ManagerName = "",
                Site = "http12s://test.com",
                CarTags = new List<long> { 1 },
                WorkTags = new List<long> { 1 },
                Logo = new FileDto
                {
                    Name = "Test.exe",
                    Path = "Test"
                },
                Photos = new List<FileDto>
                {
                    new FileDto
                    {
                        Name = "test.txt",
                        Path = "test"
                    },
                    new FileDto
                    {
                        Name = "test.raw",
                        Path = "test"
                    }
                }
            };

            var workTagsRepository = new Mock<IWorkTagsRepository>();
            workTagsRepository.Setup(act => act.Get(It.IsAny<long>())).Returns((WorkTag) null);
            _unitOfWorkMock.Setup(act => act.Repository<IWorkTagsRepository>()).Returns(workTagsRepository.Object);

            var marksRepository = new Mock<ICarMarksRepository>();
            marksRepository.Setup(act => act.Get(It.IsAny<long>())).Returns((CarMark) null);
            _unitOfWorkMock.Setup(act => act.Repository<ICarMarksRepository>()).Returns(marksRepository.Object);

            //Act
            ValidationResult result = null;
            Assert.DoesNotThrow(() => result = _manager.ValidateRegistrationCarServiceDto(dto));

            //Assert
            Assert.True(result.HasErrors);
            Assert.AreEqual(ValidationErrorResources.LoginIsNotValid, result.Errors[0]);
            Assert.AreEqual(ValidationErrorResources.PasswordsNotMatch, result.Errors[1]);
            Assert.AreEqual(ValidationErrorResources.CarServiceNameIsEmpty, result.Errors[2]);
            Assert.AreEqual(ValidationErrorResources.CarServiceAddressIsEmpty, result.Errors[3]);
            Assert.AreEqual(ValidationErrorResources.CarServiceContactEmailIsInvalid, result.Errors[4]);
            if (withPhones)
            {
                Assert.AreEqual(string.Format(ValidationErrorResources.CarServicePhoneIsIncorrect, phone1), result.Errors[5]);
                Assert.AreEqual(string.Format(ValidationErrorResources.CarServicePhoneIsIncorrect, phone2), result.Errors[6]);
                Assert.AreEqual(ValidationErrorResources.CarServiceManagerNameIsEmpty, result.Errors[7]);
                Assert.AreEqual(ValidationErrorResources.CarServiceSiteIsIncorrect, result.Errors[8]);
                Assert.AreEqual(string.Format(ValidationErrorResources.CarMarkNotFound, "1"), result.Errors[9]);
                Assert.AreEqual(string.Format(ValidationErrorResources.WorkTagNotFound, "1"), result.Errors[10]);
                Assert.AreEqual(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Logo.Name), result.Errors[11]);
                Assert.AreEqual(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Photos[0].Name), result.Errors[12]);
                Assert.AreEqual(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Photos[1].Name), result.Errors[13]);
            }
            else
            {
                Assert.AreEqual(ValidationErrorResources.CarServicePhonesIsEmpty, result.Errors[5]);
                Assert.AreEqual(ValidationErrorResources.CarServiceManagerNameIsEmpty, result.Errors[6]);
                Assert.AreEqual(ValidationErrorResources.CarServiceSiteIsIncorrect, result.Errors[7]);
                Assert.AreEqual(string.Format(ValidationErrorResources.CarMarkNotFound, "1"), result.Errors[8]);
                Assert.AreEqual(string.Format(ValidationErrorResources.WorkTagNotFound, "1"), result.Errors[9]);
                Assert.AreEqual(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Logo.Name), result.Errors[10]);
                Assert.AreEqual(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Photos[0].Name), result.Errors[11]);
                Assert.AreEqual(string.Format(ValidationErrorResources.InvalidFileExtension, dto.Photos[1].Name), result.Errors[12]);
            }
        }

        #endregion
    }
}
