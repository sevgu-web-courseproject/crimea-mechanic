using BusinessLogic.Managers;
using BusinessLogic.Managers.Abstraction;
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

        #region ValidateRegistrationUserDto


        #endregion
    }
}
