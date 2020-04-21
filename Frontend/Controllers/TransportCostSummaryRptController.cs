using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class TransportCostSummaryRptController : Controller
    {
        private string moduleCode = "TransportCostSummaryRpt";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}