using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class CashBookBalanceMngController : Controller
    {
        private string moduleCode = "CashBookBalanceMng";
        // GET: LabelingPackaging
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}