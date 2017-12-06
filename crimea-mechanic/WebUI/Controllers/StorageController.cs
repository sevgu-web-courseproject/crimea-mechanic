using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles = Common.Constants.CommonRoles.Administrator)]
    public class StorageController : BaseController
    {
        public ActionResult Cities()
        {
            return View();
        }

        public ActionResult MarksAndModels()
        {
            return View();
        }

        public ActionResult WorkClassesAndTypes()
        {
            return View();
        }
    }
}