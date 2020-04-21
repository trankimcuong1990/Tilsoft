using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ComplianceMngController : Controller
    {
        private string ModuleCode = "ComplianceMng";	

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Calendar()
        {
            ViewBag.Icon = "fa-calendar";
            ViewBag.ModuleCode = ModuleCode;          
            return View();
        }
    }
}