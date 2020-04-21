using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class WarehouseInvoiceGrossMarginRptController : Controller
    {
        private string ModuleCode = "WarehouseInvoiceGrossMarginRpt";

		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.Title = "Warehouse Invoce Gross Margin";
            ViewBag.Icon = "fa-print";

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}