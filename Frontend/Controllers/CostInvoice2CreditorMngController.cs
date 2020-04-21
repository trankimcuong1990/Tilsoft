using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class CostInvoice2CreditorMngController : Controller
    {
        string moduleCode = "CostInvoice2CreditorMng";
        // GET: CostInvoice2Creditor
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ID = id;
            ViewBag.ModuleCode = this.moduleCode;
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            return View();
        }
    }
}