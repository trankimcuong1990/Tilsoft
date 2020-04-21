using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class SalePriceOverviewRptController : Controller
    {
        // GET: SalePriceOverviewRpt
        public ActionResult Index()
        {
            return View();
        }
    }
}