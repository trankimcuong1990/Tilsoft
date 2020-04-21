using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class PurchasingPriceOverview2RptController : Controller
    {
        private string moduleCode = "PurchasingPriceOverview2Rpt";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}