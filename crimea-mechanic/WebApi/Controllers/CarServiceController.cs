using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.CarService;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [RoutePrefix("api/CarService")]
    public class CarServiceController : ApiControllerBase
    {

        #region Fields

        private readonly ICarServiceManager _manager;

        #endregion

        #region Constructor

        public CarServiceController(ICarServiceManager manager)
        {
            _manager = manager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Редактировать автосервис
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("Edit")]
        public IHttpActionResult Edit(EditCarServiceDto dto)
        {
            return CallBusinessLogicAction(() => _manager.Edit(dto, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить карточку автосервиса
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetCarServiceCard/{carServiceId:long}")]
        public IHttpActionResult GetCarServiceCard(long carServiceId)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetInfo(carServiceId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить карточку регистрации автосервиса
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("GetRegistrationCard/{carServiceId:long}")]
        public IHttpActionResult GetRegistrationCard(long carServiceId)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetRegistrationRequest(carServiceId, User.Identity.GetUserId()));
        }


        /// <summary>
        /// Получить список автосервисов
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetCarServices")]
        public IHttpActionResult GetCarServices(CarServiceFilter filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetInfos(filter));
        }

        /// <summary>
        /// Получить список запросов на регистрацию
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("GetRegistrationRequests")]
        public IHttpActionResult GetRegistratioRequests(CarServiceRegistrationsFilter filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetRegistrationRequests(filter, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Потвердить регистрацию автосервиса
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("ApproveCarService/{carServiceId:long}")]
        public IHttpActionResult ApproveCarService(long carServiceId)
        {
            return CallBusinessLogicAction(() => _manager.ApproveCarService(carServiceId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Отменить регистрацию автосервиса
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("RejectCarService/{carServiceId:long}")]
        public IHttpActionResult RejectCarService(long carServiceId)
        {
            return CallBusinessLogicAction(() => _manager.RejectCarService(carServiceId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Блокирование автосервиса
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("BlockCarService/{carServiceId:long}")]
        public IHttpActionResult BlockCarService(long carServiceId)
        {
            return CallBusinessLogicAction(() => _manager.BlockCarService(carServiceId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Разблокирование автосервиса
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("UnBlockCarService/{carServiceId:long}")]
        public IHttpActionResult UnBlockCarService(long carServiceId)
        {
            return CallBusinessLogicAction(() => _manager.UnBlockCarService(carServiceId, User.Identity.GetUserId()));
        }

        #endregion
    }
}
