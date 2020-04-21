using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class AccPIOverviewRptController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            return View();
        }
    }
}