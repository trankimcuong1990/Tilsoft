using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class StandardCostPriceOverviewRptController : Controller
    {

        string moduleCode = "StandardCostPriceOverviewRpt";
        // GET: StandardCostPriceOverviewRpt
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}