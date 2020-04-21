using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class SaleOrderMngController : Controller
    {
        private string moduleCode = "SaleOrderMng";

        // GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult PI(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.OfferID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id, int id2)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.OfferID = id2;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Overview(int id, int id2)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.OfferID = id2;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}