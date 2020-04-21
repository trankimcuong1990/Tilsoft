using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ModelController : Controller
    {
        private string moduleCode = "ModelMng";

        // GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult SampleProduct(int id, int sampleProductID)
        {
            ViewBag.Icon = "fa-file-o";
            ViewBag.ID = id;
            ViewBag.SampleProductID = sampleProductID;
            ViewBag.ModuleCode = moduleCode;

            return View();
        }
    }
}