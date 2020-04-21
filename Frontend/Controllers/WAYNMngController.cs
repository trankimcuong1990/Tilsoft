using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class WAYNMngController : Controller
    {
        private string moduleCode = "WAYNMng";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-calendar";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}