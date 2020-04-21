using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class AnnualLeaveCalendarMngController : Controller
    {
        private string ModuleCode = "AnnualLeaveCalendarMng";
        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}