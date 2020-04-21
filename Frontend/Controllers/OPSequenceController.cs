using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class OPSequenceController : Controller
    {
        private string moduleCode = "OPSequence";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = this.moduleCode;

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = this.moduleCode;

            return View();
        }
    }
}