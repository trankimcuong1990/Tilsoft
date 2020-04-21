using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class QualityInspectionRptController : Controller
    {
        private readonly string ModuleCode = "QualityInspectionRpt";

		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index(int id, string workCenterNM, int? workOrderID, int? clientID, int? productionItemID)
        {
            ViewBag.Icon = "fa-print";
            ViewBag.ModuleCode = ModuleCode;

            ViewBag.ID = id;
            ViewBag.WorkCenterNM = workCenterNM;
            ViewBag.WorkOrderID = workOrderID;
            ViewBag.ClientID = clientID;
            ViewBag.ProductionItemID = productionItemID;

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