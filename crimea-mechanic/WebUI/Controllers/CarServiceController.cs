using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CarServiceController : BaseController
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Card(long id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        public ActionResult RegistrationRequests()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
        public ActionResult RegistrationRequestCard(long id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}