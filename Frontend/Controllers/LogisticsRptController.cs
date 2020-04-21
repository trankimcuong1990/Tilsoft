using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class LogisticsRptController : Controller
    {
        private string moduleCode = "LogisticsRpt";
        // GET: LogisticsRpt
        public ActionResult Index()
        {
            return View();
        }
    }
}