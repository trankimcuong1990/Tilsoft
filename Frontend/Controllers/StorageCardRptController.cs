using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class StorageCardRptController : Controller
    {
        private string moduleCode = "StorageCardRpt";

        // GET: StorageCardRpt
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";

            return View();
        }

        public ActionResult MoveFromInvetoryOverview(int productionItemID, int factoryWarehouseID, string startDate, string endDate)
        {
            ViewBag.ProductionItemID = productionItemID;
            ViewBag.FactoryWarehouseID = factoryWarehouseID;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";

            return View();
        }
    }
}