using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ProductOverviewRptController : Controller
    {
        private string moduleCode = "ProductOverviewRpt";

        public ActionResult Index(int id) // model id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult PriceCompare(int id) // factory order detail id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}