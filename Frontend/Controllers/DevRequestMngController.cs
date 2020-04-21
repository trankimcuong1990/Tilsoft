using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class DevRequestMngController : Controller
    {
        private string moduleCode = "DevRequestMng";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = 0;
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}