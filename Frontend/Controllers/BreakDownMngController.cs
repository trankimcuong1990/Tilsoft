using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class BreakDownMngController : Controller
    {
        private string moduleCode = "BreakDownMng";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {   
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.Icon = "fa-eye";
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Init(int id)
        {
            ViewBag.Icon = "fa-file-o";
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}