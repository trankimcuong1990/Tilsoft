using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace Frontend.Controllers
{
    public class ProductionCostingRptController : Controller
    {
        private string moduleCode = "ProductionCostingRpt";
        // GET: ProductionCostingRpt
        public ActionResult Index()
        {
            return View();
        }
    }
}