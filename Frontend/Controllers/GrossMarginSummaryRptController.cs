using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class GrossMarginSummaryRptController : Controller
    {
        private string moduleCode = "GrossMarginSummaryRpt";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}