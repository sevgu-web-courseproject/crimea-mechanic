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
            if (User.IsInRole(Common.Constants.CommonRoles.CarService))
            {
                return View("ApplicationsForCarService");
            }
            if (User.IsInRole(Common.Constants.CommonRoles.Administrator))
            {
                return View("ApplicationsForAdministrator");
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
            if (User.IsInRole(Common.Constants.CommonRoles.CarService))
            {
                return View("ApplicationCardForCarService");
            }
            if (User.IsInRole(Common.Constants.CommonRoles.Administrator))
            {
                return View("ApplicationCardForAdministrator");
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