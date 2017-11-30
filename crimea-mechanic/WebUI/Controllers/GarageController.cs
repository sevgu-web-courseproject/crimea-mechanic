using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class GarageController : BaseController
    {
        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Common.Constants.CommonRoles.Regular)]
        public ActionResult Card(long id)
        {
            ViewBag.CarId = id;
            return View();
        }
    }
}