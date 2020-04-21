using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class PurchaseInvoiceMngController : Controller
    {
        private string ModuleCode = "PurchaseInvoiceMng";
        public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
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

        public ActionResult _Genneral()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/PurchaseInvoiceMng/_Edit/_Genneral.cshtml");
        }

        public ActionResult _PurchaseOrderView()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/PurchaseInvoiceMng/_Edit/_PurchaseOrderView.cshtml");
        }
    }
}