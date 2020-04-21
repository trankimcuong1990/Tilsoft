using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class RAPVNRptController : Controller
    {
        private string module = "RAPVNRpt";
        // GET: RAPVNRpt
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            ViewBag.Module = module;
            return View();
        }
    }
}