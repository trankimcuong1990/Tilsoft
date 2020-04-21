using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class PaymentNoteMngController : Controller
    {
        private string ModuleCode = "PaymentNoteMng";
		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
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

        public ActionResult _PurchasingInvoiceItem()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/PaymentNoteMng/_Edit/_PurchasingInvoiceItem.cshtml");
        }
        public ActionResult _General()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/PaymentNoteMng/_Edit/_General.cshtml");
        }

        public ActionResult _PurchaseOrderItem()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/PaymentNoteMng/_Edit/_PurchaseOrderItem.cshtml");
        }
    }
}