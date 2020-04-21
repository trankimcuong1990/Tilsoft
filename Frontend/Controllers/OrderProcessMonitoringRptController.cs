using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class OrderProcessMonitoringRptController : Controller
    {
        public ActionResult Index(int id, string t)
        {
            ViewBag.ID = id;
            ViewBag.Type = t;
            return View();
        }
    }
}