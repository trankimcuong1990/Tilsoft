using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class WarehouseOverviewRptController : Controller
    {
        private string moduleCode = "WarehouseOverviewRpt";

        // GET: WarehouseOverviewRpt
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";

            return View();
        }
    }
}