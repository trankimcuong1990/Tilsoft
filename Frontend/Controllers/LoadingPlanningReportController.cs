using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class LoadingPlanningReportController : Controller
    {
        const string ModuleCode = "LoadingPlanReportMng";
        
        // GET: LoadingPlanningReport
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;

            return View();
        }


        public ActionResult IndexDetails(int id)
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ID = id;
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}