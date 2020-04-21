using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ReportProformaInvoiceController : Controller
    {
        private string moduleCode = "ReportProformaInvoiceMng";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}