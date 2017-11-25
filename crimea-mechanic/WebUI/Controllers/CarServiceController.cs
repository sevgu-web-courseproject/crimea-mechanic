using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CarServiceController : BaseController
    {
        // GET: CarServive Card
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Card(long id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}