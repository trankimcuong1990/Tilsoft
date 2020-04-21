using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class ReceiptProductionRptController : Controller
    {
        private string moduleCode = "ReceiptProductionRpt";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.Module = moduleCode;

            return View();
        }
    }
}