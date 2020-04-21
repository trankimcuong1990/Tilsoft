using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ShipmentOverviewRptController : Controller
    {
        private string moduleCode = "ShipmentOverviewRpt";

        public ActionResult Index()
        {            
            return View();
        }
    }
}