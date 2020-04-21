using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class InventoryRptController : Controller
    {
        private string moduleCode = "InventoryRpt";

        /// <summary>
        /// Inventory: Index
        /// Condition: Start date, End date, Factory warehouse, etc.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-file-excel-o";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }
    }
}