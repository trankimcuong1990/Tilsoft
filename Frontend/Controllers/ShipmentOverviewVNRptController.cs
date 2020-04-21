using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ShipmentOverviewVNRptController : Controller
    {
        private string moduleCode = "ShipmentOverviewVNRpt";

        public ActionResult Index()
        {
            return View();
        }
    }
}