using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class PreOrderPlanningMngController : Controller
    {
        private string ModuleCode = "PreOrderPlanningMng";

		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.Icon = "fa-database";
            ViewBag.Title = "Pre-Order Planning Management";

            return View();
        }

        public ActionResult FromFactory(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.FactoryID = id;
            ViewBag.Icon = "fa-database";
            ViewBag.Title = "Pre-Order Planning Management";

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