using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Angular.app
{
    public class FactoryOrderProductionStatusRptController : Controller
    {
        private string moduleCode = "FactoryOrderProductionStatusRpt";
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}