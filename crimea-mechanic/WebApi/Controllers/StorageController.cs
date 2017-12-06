using System.Web.Http;
using BusinessLogic.Managers.Abstraction;
using BusinessLogic.Objects.Storage;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Storage")]
    public class StorageController : ApiControllerBase
    {

        private readonly IStorageManager _manager;

        public StorageController(IStorageManager storageManager)
        {
            _manager = storageManager;
        }

        [HttpGet]
        [Route("GetMarks")]
        public IHttpActionResult GetMarks(string searchText = null)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetCarMarks(searchText));
        }

        [HttpGet]
        [Route("GetModels/{markId:long}")]
        public IHttpActionResult GetModels(long markId)
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetModels(markId));
        }

        [HttpGet]
        [Route("GetCities")]
        public IHttpActionResult GetCities()
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetCities());
        }

        [HttpGet]
        [Route("GetWorkClasses")]
        public IHttpActionResult GetWorkClasses()
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetWorkClasses());
        }

        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("AddCity")]
        public IHttpActionResult AddCity(AddCityDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddCity(dto, User.Identity.GetUserId()));
        }

        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("AddCarMark")]
        public IHttpActionResult AddCarMark(AddCarMarkDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddCarMark(dto, User.Identity.GetUserId()));
        }

        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("AddCarModel")]
        public IHttpActionResult AddCarModel(AddCarModelDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddCarModel(dto, User.Identity.GetUserId()));
        }

        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("AddWorkClass")]
        public IHttpActionResult AddWorkClass(AddWorkClassDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddWorkClass(dto, User.Identity.GetUserId()));
        }

        [HttpPost]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("AddWorkType")]
        public IHttpActionResult AddWorkType(AddWorkTypeDto dto)
        {
            return CallBusinessLogicAction(() => _manager.AddWorkType(dto, User.Identity.GetUserId()));
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("DeleteMark/{markId}")]
        public IHttpActionResult DeleteMark(long markId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteMark(markId, User.Identity.GetUserId()));
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("DeleteModel/{modelId}")]
        public IHttpActionResult DeleteModel(long modelId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteModel(modelId, User.Identity.GetUserId()));
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("DeleteCity/{cityId}")]
        public IHttpActionResult DeleteCity(long cityId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteCity(cityId, User.Identity.GetUserId()));
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("DeleteWorkClass/{workClassId}")]
        public IHttpActionResult DeleteWorkClass(long workClassId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteWorkClass(workClassId, User.Identity.GetUserId()));
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        [Route("DeleteWorkType/{workTypeId}")]
        public IHttpActionResult DeleteWorkType(long workTypeId)
        {
            return CallBusinessLogicAction(() => _manager.DeleteWorkType(workTypeId, User.Identity.GetUserId()));
        }
    }
}
