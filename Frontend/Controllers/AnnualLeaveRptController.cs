using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class AnnualLeaveRptController : Controller
    {
        private string ModuleCode = "AnnualLeaveRpt";
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";           
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}