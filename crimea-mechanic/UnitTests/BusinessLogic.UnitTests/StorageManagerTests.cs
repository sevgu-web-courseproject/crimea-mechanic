using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects;
using BusinessLogic.Resources;
using BusinessLogic.UnitTests.Utils;
using Common.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Abstraction;
using DependencyInjector;
using Moq;
using NUnit.Framework;

namespace BusinessLogic.UnitTests
{
    [TestFixture(Description = "Тесты для StorageManager")]
    public class StorageManagerTests
    {
        #region Fields

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserInternalManager> _userManager;
        private StorageManager _manager;

        #endregion

        #region Init

        [OneTimeSetUp]
        public void ConfigureSet()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userManager = new Mock<IUserInternalManager>();
            _manager = new StorageManager(_unitOfWorkMock.Object, _userManager.Object);

            InitializationAutomapper.Init();
        }

        [SetUp]
        public void Init()
        {
            _unitOfWorkMock.Reset();
            _userManager.Reset();
        }

        #endregion

        #region GetCarMarks

        [Test(Description = "GetCarMarks должен вернуть все марки автомобилей")]
        public void GetCarMarks_Must_Return_All_Car_Marks()
        {
            //Arrange

            var marks = new List<CarMark>
            {
                CarMarksUtils.Create(1, "Aaaa"),
                CarMarksUtils.Create(2, "Bbbb"),
                CarMarksUtils.Create(3, "Cccc", true),
                CarMarksUtils.Create(4, "Dddd", true),
                CarMarksUtils.Create(5, "Eeee")
            };

            var repository = new Mock<ICarMarksRepository>();
            repository.Setup(act => act.GetAll(true)).Returns(marks.AsQueryable());
            _unitOfWorkMock.Setup(act => act.Repository<ICarMarksRepository>()).Returns(repository.Object);

            //Act
            var result = _manager.GetCarMarks(null);

            //Assert
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(marks[0].Name, result.ElementAt(0).Name);
            Assert.AreEqual(marks[1].Name, result.ElementAt(1).Name);
            Assert.AreEqual(marks[4].Name, result.ElementAt(2).Name);
        }

        [Test(Description = "GetCarMarks должен вернуть все марки автомобилей содержащие текст поиска")]
        public void GetCarMarks_Must_Return_All_Car_Marks_Witch_Contains_SearhText()
        {
            //Arrange

            var searchText = "Aa";
            var marks = new List<CarMark>
            {
                CarMarksUtils.Create(1, "Aaaa"),
                CarMarksUtils.Create(2, "Bbbb"),
                CarMarksUtils.Create(3, "Aacc", true),
                CarMarksUtils.Create(4, "DdAa", true),
                CarMarksUtils.Create(5, "Aaee"),
                CarMarksUtils.Create(6, "FfAa"),
                CarMarksUtils.Create(7, "GgAaHh")
            };

            var repository = new Mock<ICarMarksRepository>();
            repository.Setup(act => act.GetAll(true)).Returns(marks.AsQueryable());
            _unitOfWorkMock.Setup(act => act.Repository<ICarMarksRepository>()).Returns(repository.Object);

            //Act
            var result = _manager.GetCarMarks(searchText);

            //Assert
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(marks[0].Name, result.ElementAt(0).Name);
            Assert.AreEqual(marks[4].Name, result.ElementAt(1).Name);
            Assert.AreEqual(marks[5].Name, result.ElementAt(2).Name);
            Assert.AreEqual(marks[6].Name, result.ElementAt(3).Name);
        }

        #endregion

        #region GetModels

        [Test(Description = "GetModels должен бросить ошибку если не найдена марка автомобиля")]
        public void GetModels_Must_Throw_Exception_When_Car_Mark_Not_Found()
        {
            //Arrange
            var markId = 1;
            var repository = new Mock<ICarMarksRepository>();
            repository.Setup(act => act.Get(markId)).Returns((CarMark) null);
            _unitOfWorkMock.Setup(act => act.Repository<ICarMarksRepository>()).Returns(repository.Object);

            //Act
            var exception = Assert.Throws<BusinessFaultException>(() => _manager.GetModels(markId));

            //Assert
            Assert.AreEqual(BusinessLogicExceptionResources.CarMarkNotFound, exception.Message);
            _unitOfWorkMock.Verify(act => act.Repository<ICarMarksRepository>(), Times.Once);
            _unitOfWorkMock.Verify(act => act.Repository<ICarModelsRepository>(), Times.Never);
            repository.Verify(act => act.Get(markId), Times.Once);
        }

        [Test(Description = "GetModels должен вернуть все модели определенной марки автомобиля без ошибок")]
        public void GetModels_Must_Returns_All_Models_For_A_Certain_Mark_Without_Errors()
        {
            //Arrange
            var mark = CarMarksUtils.Create(1, "test");
            mark.Models = new List<CarModel>
            {
                CarModelUtils.Create(0, "Aaaa"),
                CarModelUtils.Create(1, "Bbbb"),
                CarModelUtils.Create(2, "Cccc", true),
                CarModelUtils.Create(3, "Dddd"),
                CarModelUtils.Create(4, "Eeee", true)
            };

            var markRepository = new Mock<ICarMarksRepository>();
            markRepository.Setup(act => act.Get(mark.Id)).Returns(mark);
            _unitOfWorkMock.Setup(act => act.Repository<ICarMarksRepository>()).Returns(markRepository.Object);

            //Act
            IEnumerable<CarModelDto> result = null;
            Assert.DoesNotThrow(() => result = _manager.GetModels(mark.Id));

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(mark.Models.ElementAt(0).Name, result.ElementAt(0).Name);
            Assert.AreEqual(mark.Models.ElementAt(1).Name, result.ElementAt(1).Name);
            Assert.AreEqual(mark.Models.ElementAt(3).Name, result.ElementAt(2).Name);
        }

        #endregion

        #region GetWorkTags

        [Test(Description = "GetWorkTags должен вернуть все виды работы без ошибок")]
        public void GetWorkTags_Must_Return_All_Work_Tags_Without_Errors()
        {
            //Arrange
            var workTags = new List<WorkTag>
            {
                WorkTagsUtils.Create(0, "Aaaa", true),
                WorkTagsUtils.Create(1, "Bbbb", true),
                WorkTagsUtils.Create(2, "Cccc"),
                WorkTagsUtils.Create(3, "Dddd"),
                WorkTagsUtils.Create(4, "Eeee")
            };

            var repository = new Mock<IWorkTagsRepository>();
            repository.Setup(act => act.GetAll(true)).Returns(workTags.AsQueryable);
            _unitOfWorkMock.Setup(act => act.Repository<IWorkTagsRepository>()).Returns(repository.Object);

            //Act
            IEnumerable<WorkTagDto> result = null;
            Assert.DoesNotThrow(() => result = _manager.GetWorkTags());

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(workTags[2].Name, result.ElementAt(0).Name);
            Assert.AreEqual(workTags[3].Name, result.ElementAt(1).Name);
            Assert.AreEqual(workTags[4].Name, result.ElementAt(2).Name);
        }

        #endregion
    }
}
