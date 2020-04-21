using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class PriceConfigurationController : Controller
    {
        private string moduleCode = "PriceConfiguration";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;

            return View();
        }
    }
}