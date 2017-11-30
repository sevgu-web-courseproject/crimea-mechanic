using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ApplicationController : BaseController
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.IsInRole(Common.Constants.CommonRoles.Regular))
            {
                return View("ApplicationForUser");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Card(long id)
        {
            ViewBag.Id = id;
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.IsInRole(Common.Constants.CommonRoles.Regular))
            {
                return View("ApplicationCardForUser");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Common.Constants.CommonRoles.CarService)]
        public ActionResult Pool()
        {
            return View();
        }
    }
}