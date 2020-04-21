using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class DevelopmentItemsRptController : Controller
    {
        string moduleCode = "DevelopmentItemsRpt";
        // GET: DevelopmentItemsRpt
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }
    }
}