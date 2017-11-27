using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Car;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Car")]
    public class CarController : ApiControllerBase
    {
        #region Fields

        private readonly ICarManager _manager;

        #endregion

        #region Constructor

        public CarController(ICarManager carManager)
        {
            _manager = carManager;
        }

        #endregion

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("AddCar")]
        [HttpPost]
        public IHttpActionResult AddCar(AddOrEditUserCarDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddCar(dto, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("EditCar")]
        [HttpPost]
        public IHttpActionResult EditCar(AddOrEditUserCarDto dto)
        {
            return CallBusinessLogicAction(() => _manager.EditCar(dto, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("DeleteCar")]
        [HttpGet]
        public IHttpActionResult DeleteCar(long id)
        {
            return CallBusinessLogicAction(() => _manager.DeleteCar(id, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("RestoreCar")]
        [HttpGet]
        public IHttpActionResult RestoreCar(long id)
        {
            return CallBusinessLogicAction(() => _manager.RestoreCar(id, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("GetCar")]
        [HttpGet]
        public IHttpActionResult GetCar(long id)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetCar(id, User.Identity.GetUserId()));
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        [Route("GetCars")]
        [HttpPost]
        public IHttpActionResult GetCar(FilterUserCar filter)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetCars(filter, User.Identity.GetUserId()));
        }
    }
}
