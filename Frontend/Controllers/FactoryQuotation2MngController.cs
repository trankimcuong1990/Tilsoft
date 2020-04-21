using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class FactoryQuotation2MngController : Controller
    {
        private string ModuleCode = "FactoryQuotation2Mng";

        public ActionResult Index()
        {
            if (Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION] == null || Session[Frontend.Customize.Common.ProjectDefinition.TOKEN_SESSION] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Frontend.APIDTO.APIUserInformation userInfo = (Frontend.APIDTO.APIUserInformation)Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            ViewBag.CurrentLoginUserInfo = userInfo;
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}