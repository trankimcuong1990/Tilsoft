using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class ProductBreakDownPALMngController : Controller
    {
        private readonly string moduleCode = "ProductBreakDownPALMng";

		public ActionResult Init()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id, int? modelID, int? sampleProductID)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            ViewBag.ModelID = modelID;
            ViewBag.SampleProductID = sampleProductID;
            return View();
        }

        public ActionResult View(int id, int? modelID, int? sampleProductID)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            ViewBag.ModelID = modelID;
            ViewBag.SampleProductID = sampleProductID;
            return View();
        }

        public ActionResult Copy(int id, int oldID)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            ViewBag.OldID = oldID;
            return View();
        }
    }
}