using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class StandardPurchasingPriceMngController : Controller
    {
        private string moduleCode = "StandardPurchasingPriceMng";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}