using System.Web.Http;
using BusinessLogic.Managers.Abstraction;

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
        [Route("GetWorkTags")]
        public IHttpActionResult GetWorkTags()
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetWorkTags());
        }

        [HttpGet]
        [Route("GetCities")]
        public IHttpActionResult GetCities()
        {
            return CallBusinessLogicActionWithResult(() => _manager.GetCities());
        }
    }
}
