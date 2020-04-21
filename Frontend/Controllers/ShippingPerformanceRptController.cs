using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class ShippingPerformanceRptController : Controller
    {
        private string moduleCode = "ShippingPerformanceRpt";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-bar-chart-o";
            return View();
        }
    }
}