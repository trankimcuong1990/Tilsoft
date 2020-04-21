using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientPortal.Library;

namespace ClientPortal.Controllers
{
    [AVSAuthorize]
    public class LoadingPlanController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }
    }
}