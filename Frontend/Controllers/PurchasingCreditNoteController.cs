using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class PurchasingCreditNoteController : Controller
    {
        private string moduleCode = "PurchasingCreditNote";

        //GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.PurchasingInvoiceID = Request.QueryString["purchasingInvoiceID"];
            ViewBag.SupplierID = Request.QueryString["supplierID"];
            ViewBag.CreditNoteType = Request.QueryString["creditNoteType"];
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Init()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

    }
}