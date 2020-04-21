using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class InventoryCostRptController : Controller
    {
        private string moduleCode = "InventoryCostRpt";
        // GET: InventoryCostRpt
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-file-excel-o";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}