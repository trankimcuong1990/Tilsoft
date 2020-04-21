using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class SpecificationOfProductMngController : Controller
    {
        string moduleCode = "SpecificationOfProductMng";
        // GET: SpecificationOfProductMng
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id, int? sampleProductID, int? productID)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            ViewBag.SampleProductID = sampleProductID;
            ViewBag.ProductID = productID;
            return View();
        }

        public ActionResult init()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

    }
}