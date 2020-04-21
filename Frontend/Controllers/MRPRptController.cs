using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class MRPRptController : Controller
    {
        private string ModuleCode = "MRPRpt";
		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id, string fromDate, string toDate)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
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