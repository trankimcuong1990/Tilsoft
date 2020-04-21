using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class FSCRptController : Controller
    {
        private string moduleCode = "FSCRpt";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-file-excel-o";

            return View();
        }
    }
}