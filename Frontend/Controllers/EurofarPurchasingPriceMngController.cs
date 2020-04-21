using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class EurofarPurchasingPriceMngController : Controller
    {
        private string ModuleCode = "EurofarPurchasingPriceMng";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.CurrentLoginUserInfo = Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];
            return View();
        }
    }
}