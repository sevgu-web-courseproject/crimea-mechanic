using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Application;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Application")]
    public class ApplicationController : ApiControllerBase
    {
        #region Fields

        private readonly IApplicationManager _manager;

        #endregion

        #region Constructor

        public ApplicationController(IApplicationManager manager)
        {
            _manager = manager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Создание новой заявки пользователем
        /// </summary>
        /// <param name="dto">ДТО</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(CreateApplicationDto dto)
        {
            return CallBusinessLogicAction(() => _manager.CreateApplication(dto, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("Edit")]
        [HttpPost]
        public IHttpActionResult Edit(EditApplicationDto dto)
        {
            return CallBusinessLogicAction(() => _manager.EditApplication(dto, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("Delete/{applicationId}")]
        [HttpGet]
        public IHttpActionResult Delete(long applicationId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteApplication(applicationId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить информацию о заявке для пользователя
        /// </summary>
        /// <param name="applicationId">Идентификатор заявки</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("GetInfoForUser/{applicationId}")]
        [HttpGet]
        public IHttpActionResult GetInfoForUser(long applicationId)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationInfoForUser(applicationId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить список заявок пользователя
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("GetInfosForUser")]
        [HttpPost]
        public IHttpActionResult GetInfosForUser(ApplicationsFilter filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationsInfoForUser(filter, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить список заявок автосервиса
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("GetInfosForService")]
        [HttpPost]
        public IHttpActionResult GetInfosForService(ApplicationsFilter filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationsForService(filter, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить список заявок для администратора
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("GetInfosForAdministrator")]
        [HttpPost]
        public IHttpActionResult GetInfosForAdministrator(ApplicationsFilter filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationsForAdministrator(filter, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить список заявок находящихся в пуле
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("GetPool")]
        [HttpPost]
        public IHttpActionResult GetPool(ApplicationsPoolFilter filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationsFromPool(filter, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить информацию о заявке для автосервиса
        /// </summary>
        /// <param name="applicationId">Идентификатор заявки</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("GetInfoForCarService/{applicationId}")]
        [HttpGet]
        public IHttpActionResult GetInfoForCarService(long applicationId)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationInfoForService(applicationId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Получить информацию о заявке для администатора
        /// </summary>
        /// <param name="applicationId">Идентификатор заявки</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("GetInfoForAdmin/{applicationId}")]
        [HttpGet]
        public IHttpActionResult GetInfoForAdmin(long applicationId)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetApplicationInfoForAdministrator(applicationId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Добавить предложение к заявке
        /// </summary>
        /// <param name="dto">ДТО</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("AddOffer")]
        [HttpPost]
        public IHttpActionResult AddOffer(AddOfferDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddOffer(dto, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Удалить предложение
        /// </summary>
        /// <param name="offerId">Идентификатор предложения</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("DeleteOffer/{offerId}")]
        [HttpGet]
        public IHttpActionResult DeleteOffer(long offerId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteOffer(offerId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Принять заявку
        /// </summary>
        /// <param name="offerId">Идентификатор предложения</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("AcceptOffer/{offerId}")]
        [HttpGet]
        public IHttpActionResult AcceptOffer(long offerId)
        {
            return CallBusinessLogicAction(() => _manager.AcceptOffer(offerId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Отменить выполнение заявки пользователем либо автосервисом
        /// </summary>
        /// <param name="applicationId">Идентификатор заявки</param>
        /// <returns></returns>
        [Authorize]
        [Route("RejectApplication/{applicationId}")]
        [HttpGet]
        public IHttpActionResult RejectApplication(long applicationId)
        {
            return CallBusinessLogicAction(() => _manager.RejectApplication(applicationId, User.Identity.GetUserId()));
        }

        /// <summary>
        /// Завершить выполнение заявки
        /// </summary>
        /// <param name="applicationId">Идентификатор заявки</param>
        /// <returns></returns>
        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("CompleteApplication/{applicationId}")]
        [HttpGet]
        public IHttpActionResult CompleteApplication(long applicationId)
        {
            return CallBusinessLogicAction(() => _manager.CompleteApplication(applicationId, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("GetMarksFromPool")]
        [HttpGet]
        public IHttpActionResult GetMarksFromPool()
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetAvailableMarksFromPool(User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        [Route("GetWorkTypesFromPool")]
        [HttpGet]
        public IHttpActionResult GetWorkTypesFromPool()
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetAvailableWorkTypesFromPool(User.Identity.GetUserId()));
        }

        #endregion
    }
}
