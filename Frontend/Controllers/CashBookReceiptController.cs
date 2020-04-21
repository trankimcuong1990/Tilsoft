namespace Frontend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CashBookReceiptController : Controller
    {
        private string moduleCode = "CashBookReceipt";

        // GET: CashBookReceipt
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil";
            ViewBag.ID = id;

            return View();
        }
    }
}