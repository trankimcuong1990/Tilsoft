using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class PurchasingCalendarMngController : Controller
    {
        private string moduleCode = "PurchasingCalendarMng";
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}