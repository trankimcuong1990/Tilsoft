using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class ShowroomRptController : Controller
    {
        string moduleCode = "ShowroomRpt";
        // GET: ShowroomRpt
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }

        public ActionResult Init()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }
    }
}