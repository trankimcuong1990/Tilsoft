using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class ProductBreakDownMngController : Controller
    {
        private string moduleCode = "ProductBreakDownMng";

        public ActionResult Edit(int id, int? modelID, int? sampleProductID)
        {
            ViewBag.ID = id;
            ViewBag.ModelID = modelID;
            ViewBag.SampleProductID = sampleProductID;
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }
    }
}