using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Car;
using BusinessLogic.Resources;
using BusinessLogic.UnitTests.Utils;
using Common.Enums;
using Common.Exceptions;
using Common.Validation;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;
using DependencyInjector;
using Moq;
using NUnit.Framework;

namespace BusinessLogic.UnitTests
{
    [TestFixture(Description = "Тесты для CarManagerTests")]
    public class CarManagerTests
    {
        #region Fields

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserInternalManager> _userManagerMock;
        private Mock<IValidationManager> _validationManagerMock;
        private ICarManager _manager;

        #endregion

        #region Init

        [OneTimeSetUp]
        public void ConfigureSet()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userManagerMock = new Mock<IUserInternalManager>();
            _validationManagerMock = new Mock<IValidationManager>();
            _manager = new CarManager(_unitOfWorkMock.Object, _userManagerMock.Object, _validationManagerMock.Object);

            InitializationAutomapper.Init();
        }

        [SetUp]
        public void Init()
        {
            _unitOfWorkMock.Reset();
            _userManagerMock.Reset();
            _validationManagerMock.Reset();
        }

        #endregion

        #region AddCar

        [Test(Description = "AddCar должен добавить пользователю машину")]
        public void AddCar_Must_Add_Car_To_User()
        {
            //Arrange
            var userId = "1";
            var user = new ApplicationUser
            {
                Id = userId,
                UserProfile = new UserProfile
                {
                    Cars = new List<UserCar>()
                }
            };
            var dto = new AddOrEditUserCarDto
            {
                Name = "Test",
                ModelId = 1,
                Year = 2015,
                Vin = "0123456789ABCDEFG",
                FuelType = FuelType.Diesel,
                EngineCapacity = 2.0f
            };

            var validationResult = new ValidationResult("test");

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));
            _userManagerMock.Setup(act => act.CheckAndGet(userId)).Returns(user);
            _validationManagerMock.Setup(act => act.ValidateUserCarDto(dto)).Returns(validationResult);

            var repository = new Mock<ICarModelsRepository>();
            repository.Setup(act => act.Get(dto.ModelId.Value)).Returns(new CarModel());
            _unitOfWorkMock.Setup(act => act.Repository<ICarModelsRepository>()).Returns(repository.Object);

            //Act
            Assert.DoesNotThrow(() => _manager.AddCar(dto, userId));

            //Assert
            _validationManagerMock.Verify(act => act.ValidateUserCarDto(dto), Times.Once);
            repository.Verify(act => act.Get(dto.ModelId.Value), Times.Once);
            _unitOfWorkMock.Verify(act => act.Repository<ICarModelsRepository>(), Times.Once);
            _unitOfWorkMock.Verify(act => act.SaveChanges(), Times.Once);
        }

        [Test(Description = "AddCar должен бросить ошибку если дто не прошло валидацию")]
        public void AddCar_Must_Throw_Exception_When_Dto_Did_Not_Pass_Validation()
        {
            //Arrange
            var userId = "1";
            var dto = new AddOrEditUserCarDto();

            var validationResult = new ValidationResult("test");
            validationResult.AddError("test");

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));
            _userManagerMock.Setup(act => act.CheckAndGet(userId)).Returns(new ApplicationUser());
            _validationManagerMock.Setup(act => act.ValidateUserCarDto(dto)).Returns(validationResult);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.AddCar(dto, userId));

            //Assert
            _validationManagerMock.Verify(act => act.ValidateUserCarDto(dto), Times.Once);
            Assert.AreEqual(validationResult.GetErrors(), exception.Message);
        }

        #endregion

        #region EditCar

        [Test(Description = "EditCar должен отредактировать машину пользователя")]
        public void EditCar_Must_Edit_User_Car()
        {
            //Arrange
            var userId = "1";
            var dto = new AddOrEditUserCarDto
            {
                Id = 1,
                Name = "test",
                Year = 2006,
                Vin = "0123456789ABCDEFG",
                FuelType = FuelType.Benzine,
                EngineCapacity = 1.6f
            };

            var validationResult = new ValidationResult("test");
            var userCar = UserCarUtils.Create(1, "TEST", "modelName", "markName", userId);

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));
            _validationManagerMock.Setup(act => act.ValidateUserCarDto(dto)).Returns(validationResult);

            var userCarRepository = new Mock<IUserCarRepository>();
            userCarRepository.Setup(act => act.Get(dto.Id.Value)).Returns(userCar);
            userCarRepository.Setup(act => act.Update(userCar));
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(userCarRepository.Object);

            //Act
            Assert.DoesNotThrow(() => _manager.EditCar(dto, userId));

            //Assert
            _validationManagerMock.Verify(act => act.ValidateUserCarDto(dto), Times.Once);
            userCarRepository.Verify(act => act.Get(dto.Id.Value), Times.Once);
            _unitOfWorkMock.Verify(act => act.Repository<IUserCarRepository>(), Times.Once);
            userCarRepository.Verify(act => act.Update(userCar), Times.Once);
            _unitOfWorkMock.Verify(act => act.SaveChanges(), Times.Once);
        }

        [Test(Description = "EditCar должен бросить ошибку если машина не принадлежит пользователю")]
        public void EditCar_Must_Throw_Exception_When_Car_Not_Found()
        {
            //Arrange
            var userId = "1";
            var dto = new AddOrEditUserCarDto
            {
                Id = 1
            };

            var validationResult = new ValidationResult("test");
            var userCar = new UserCar
            {
                User = new UserProfile
                {
                    ApplicationUser = new ApplicationUser
                    {
                        Id = "another"
                    }
                }
            };

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));
            _validationManagerMock.Setup(act => act.ValidateUserCarDto(dto)).Returns(validationResult);

            var userCarRepository = new Mock<IUserCarRepository>();
            userCarRepository.Setup(act => act.Get(dto.Id.Value)).Returns(userCar);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(userCarRepository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.EditCar(dto, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.CarDoesNotBelongToUser, exception.Message);
            _validationManagerMock.Verify(act => act.ValidateUserCarDto(dto), Times.Once);
            userCarRepository.Verify(act => act.Get(dto.Id.Value), Times.Once);
            _unitOfWorkMock.Verify(act => act.Repository<IUserCarRepository>(), Times.Once);
        }

        [Test(Description = "EditCar должен бросить ошибку если дто не прошло валидацию")]
        public void EditCar_Must_Throw_Exception_When_Dto_Not_Pass_Validation()
        {
            //Arrange
            var userId = "1";
            var dto = new AddOrEditUserCarDto();

            var validationResult = new ValidationResult("test");
            validationResult.AddError("test");

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));
            _validationManagerMock.Setup(act => act.ValidateUserCarDto(dto)).Returns(validationResult);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.EditCar(dto, userId));

            //Assert
            _validationManagerMock.Verify(act => act.ValidateUserCarDto(dto), Times.Once);
            Assert.AreEqual(validationResult.GetErrors(), exception.Message);
        }

        #endregion

        #region DeleteCar

        [Test(Description = "DeleteCar должен удалить машину пользователя")]
        public void DeleteCar_Must_Delete_User_Car()
        {
            //Arrange
            var userId = "1";
            var carId = 1;
            var userCar = UserCarUtils.CreateWithUser(userId);

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns(userCar);
            repository.Setup(act => act.Update(userCar));
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            Assert.DoesNotThrow(() => _manager.DeleteCar(carId, userId));

            //Assert
            repository.Verify(act => act.Get(carId), Times.Once);
            repository.Verify(act => act.Update(userCar), Times.Once);
            _unitOfWorkMock.Verify(act => act.Repository<IUserCarRepository>(), Times.Once);
            _unitOfWorkMock.Verify(act => act.SaveChanges(), Times.Once);
        }

        [Test(Description = "DeleteCar должен бросить ошибку если машина пользователя не найдена")]
        public void DeleteCar_Must_Throw_Exception_When_User_Car_Not_Found()
        {
            //Arrange
            var userId = "1";
            var carId = 1;

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns((UserCar)null);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.DeleteCar(carId, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.UserCarNotFound, exception.Message);
        }

        [Test(Description = "DeleteCar должен бросить ошибку если машина не принадлежит пользователю")]
        public void DeleteCar_Must_Throw_Exception_When_Car_Does_Not_Belong_To_User()
        {
            //Arrange
            var userId = "1";
            var carId = 1;
            var userCar = UserCarUtils.CreateWithUser("another");

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns(userCar);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.DeleteCar(carId, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.CarDoesNotBelongToUser, exception.Message);
        }

        #endregion

        #region RestoreCar

        [Test(Description = "RestoreCar должен восстановить машину пользователя")]
        public void RestoreCar_Must_Restore_User_Car()
        {
            //Arrange
            var userId = "1";
            var carId = 1;
            var userCar = UserCarUtils.CreateWithUser(userId);

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns(userCar);
            repository.Setup(act => act.Update(userCar));
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            Assert.DoesNotThrow(() => _manager.RestoreCar(carId, userId));

            //Assert
            repository.Verify(act => act.Get(carId), Times.Once);
            repository.Verify(act => act.Update(userCar), Times.Once);
            _unitOfWorkMock.Verify(act => act.Repository<IUserCarRepository>(), Times.Once);
            _unitOfWorkMock.Verify(act => act.SaveChanges(), Times.Once);
        }

        [Test(Description = "RestoreCar должен бросить ошибку если машина не найдена")]
        public void RestoreCar_Must_Throw_Exception_When_User_Car_Not_Found()
        {
            //Arrange
            var userId = "1";
            var carId = 1;

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns((UserCar)null);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.RestoreCar(carId, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.UserCarNotFound, exception.Message);
        }

        [Test(Description = "RestoreCar должен бросить ошибку если машина не принадлежит пользователю")]
        public void RestoreCar_Must_Throw_Exception_When_User_Car_Does_Not_Belong_To_User()
        {
            //Arrange
            var userId = "1";
            var carId = 1;
            var userCar = UserCarUtils.CreateWithUser("another");

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns(userCar);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.DeleteCar(carId, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.CarDoesNotBelongToUser, exception.Message);
        }

        #endregion

        #region GetCar

        [Test(Description = "GetCar должен вернуть информацию о машине пользователя")]
        public void GetCar_Must_Return_Info_For_User_Car()
        {
            //Arrange
            var userId = "1";
            var carId = 1;
            var userCar = UserCarUtils.Create(carId, "test", "testModel", "testMark", userId);

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns(userCar);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var result = _manager.GetCar(carId, userId);

            //Assert
            Assert.AreEqual(userCar.Id, result.Id);
            Assert.AreEqual(userCar.Name, result.Name);
            Assert.AreEqual(userCar.Model.Name, result.Model);
            Assert.AreEqual(userCar.Model.Mark.Name, result.Mark);
            Assert.AreEqual(userCar.Year.ToString(), result.Year);
            Assert.AreEqual(userCar.Vin, result.Vin);
            Assert.AreEqual("Дизель", result.FuelType);
            Assert.AreEqual(userCar.EngineCapacity, result.EngineCapacity);
        }

        [Test(Description = "GetCar должна бросить ошибку если машина пользователя не найдена")]
        public void GetCar_Must_Throw_Exception_When_User_Car_Not_Found()
        {
            //Arrange
            var userId = "1";
            var carId = 1;

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns((UserCar) null);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.GetCar(carId, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.UserCarNotFound, exception.Message);
        }

        [Test(Description = "GetCar должна бросить ошибку если машина не принадлежит пользователю")]
        public void GetCar_Must_Throw_Exception_When_Car_Does_Not_Belong_To_User()
        {
            //Arrange
            var userId = "1";
            var carId = 1;
            var userCar = UserCarUtils.CreateWithUser("another");

            _userManagerMock.Setup(act => act.IsUserInRegularRole(userId));

            var repository = new Mock<IUserCarRepository>();
            repository.Setup(act => act.Get(carId)).Returns(userCar);
            _unitOfWorkMock.Setup(act => act.Repository<IUserCarRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.GetCar(carId, userId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.CarDoesNotBelongToUser, exception.Message);
        }

        #endregion

        #region GetCars

        [Test(Description = "GetCars должен вернуть данные по машинам пользователя")]
        [TestCase(true)]
        [TestCase(false)]
        public void GetCars_Must_Return_Info_For_User_Cars(bool showDeleted)
        {
            //Arrange
            var cars = new List<UserCar>
            {
                UserCarUtils.Create(1, "Test1", "TestModel1", "TestMark1", "1"),
                UserCarUtils.Create(2, "Test2", "TestModel2", "TestMark2", "1"),
                UserCarUtils.Create(3, "Test3", "TestModel3", "TestMark3", "1", true),
                UserCarUtils.Create(4, "Test4", "TestModel4", "TestMark4", "1", true)

            };

            var filter = new FilterUserCar
            {
                CurrentPage = 1,
                ItemsPerPage = 10
            };

            var user = new ApplicationUser
            {
                Id = "1",
                UserProfile = new UserProfile
                {
                    Cars = cars
                }
            };

            _userManagerMock.Setup(act => act.IsUserInRegularRole(user.Id));
            _userManagerMock.Setup(act => act.CheckAndGet(user.Id)).Returns(user);

            //Act
            var result = _manager.GetCars(filter, user.Id);

            //Assert
            Assert.AreEqual(result.ItemsCount, 2);
            if (!showDeleted)
            {
                Assert.AreEqual(cars[0].Name, result.Items.ElementAt(0).Name);
                Assert.AreEqual(cars[1].Name, result.Items.ElementAt(1).Name);
                Assert.AreEqual(cars[0].Model.Name, result.Items.ElementAt(0).Model);
                Assert.AreEqual(cars[1].Model.Mark.Name, result.Items.ElementAt(1).Mark);
            }
            else
            {
                Assert.AreEqual(cars[2].Name, result.Items.ElementAt(0).Name);
                Assert.AreEqual(cars[3].Name, result.Items.ElementAt(1).Name);
                Assert.AreEqual(cars[2].Model.Name, result.Items.ElementAt(0).Model);
                Assert.AreEqual(cars[3].Model.Mark.Name, result.Items.ElementAt(1).Mark);
            }
        }

        #endregion
    }
}
