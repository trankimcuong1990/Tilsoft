using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class AccManagerPerformanceRptController : Controller
    {
        private string ModuleCode = "AccManagerPerformanceRpt";

        public ActionResult Index()
        {
            if (Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION] == null || Session[Frontend.Customize.Common.ProjectDefinition.TOKEN_SESSION] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.CurrentLoginUserInfo = (Frontend.APIDTO.APIUserInformation)Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            ViewBag.Token = Session[Frontend.Customize.Common.ProjectDefinition.TOKEN_SESSION];
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}