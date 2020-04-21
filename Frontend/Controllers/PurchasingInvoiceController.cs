using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class PurchasingInvoiceController : Controller
    {
        private string moduleCode = "PurchasingInvoiceMng";

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
            //ViewBag.InvoiceType = Request.QueryString["invoiceType"];
            //ViewBag.BookingID = Request.QueryString["bookingID"];
            //ViewBag.SupplierID = Request.QueryString["supplierID"];
            //ViewBag.ParentID = Request.QueryString["parentID"];
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