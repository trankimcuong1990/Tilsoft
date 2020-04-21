using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class RapListController : Controller
    {
        const string ModuleCode = "RapMng";
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;

            return View();
        }
    }
}