using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class TransportCostForwarderController : Controller
    {
        private string moduleCode = "TransportCostForwarder";

        // GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

    }
}