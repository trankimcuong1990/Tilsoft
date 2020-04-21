using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class Sample3MngController : Controller
    {
        private string moduleCode = "Sample3Mng";

        //
        // ORDER
        //
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult OrderInit()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult OrderOverview(int id)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult OrderEdit(int id)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }

        //
        // PRODUCT
        //
        public ActionResult ProductOverview(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult QARemarkList(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult InternalRemarkList(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult TechnicalDrawingList(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult FactoryAssignment(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult ItemData(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult BuildingProcess(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult ProductInfo(int id) // sample product id
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}