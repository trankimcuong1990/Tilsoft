using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class FactoryOrderController : Controller
    {
        private string moduleCode = "FactoryOrderMng";

        // GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult FaO(int id, int id2)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.SaleOrderID = id;
            ViewBag.OfferID = id2;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id, int id2)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.SaleOrderID = id2;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
        public ActionResult OverView(int id, int id2)
        {
            ViewBag.Icon = "fa-eye";
            ViewBag.ID = id;
            ViewBag.SaleOrderID = id2;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}