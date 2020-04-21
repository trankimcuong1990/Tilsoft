using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class UsageTrackingRptController : Controller
    {
        private string moduleCode = "UsageTrackingRpt";
        
        public ActionResult Index()
        {
            string employeeID = Request.QueryString["employeeID"];
            if(employeeID != null)
            {
                ViewBag.employeeID = employeeID;
            }
            else
            {
                ViewBag.employeeID = -1;
            }
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}