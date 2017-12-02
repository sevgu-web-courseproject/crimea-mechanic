using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        public ActionResult MyProfile()
        {
            return View("Profile");
        }
    }
}