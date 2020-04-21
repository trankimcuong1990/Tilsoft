using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            if (Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION] == null || Session[Frontend.Customize.Common.ProjectDefinition.TOKEN_SESSION] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Frontend.APIDTO.APIUserInformation userInfo = (Frontend.APIDTO.APIUserInformation)Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            //if (userInfo.Data.CompanyID == 2) // auvietsoft company
            //{
            //    return RedirectToAction("ASVDashboard");
            //}
            if (userInfo.Data.UserGroupID == 1)
            {
                return RedirectToAction("AdminDashboard");
            }

            return RedirectToAction("UserDashboard");
        }

        public ActionResult AdminDashboard()
        {
            Frontend.APIDTO.APIUserInformation userInfo = (Frontend.APIDTO.APIUserInformation)Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            if(userInfo.Data.UserGroupID != 1) // not belong to admin group
                return RedirectToAction("UserDashboard");

            ViewBag.CurrentLoginUserInfo = userInfo;
            ViewBag.Token = Session[Frontend.Customize.Common.ProjectDefinition.TOKEN_SESSION];
            return View();
        }

        public ActionResult UserDashboard()
        {
            ViewBag.CurrentLoginUserInfo = (Frontend.APIDTO.APIUserInformation)Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            ViewBag.Token = Session[Frontend.Customize.Common.ProjectDefinition.TOKEN_SESSION];
            return View();
        }

        public ActionResult ASVDashboard()
        {
            return View();
        }
    }
}