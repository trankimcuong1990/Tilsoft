using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class PermissionOverviewRptController : Controller
    {
        private string moduleCode = "PermissionOverviewRpt";

        public ActionResult Index()
        {            
            return View();
        }
    }
}