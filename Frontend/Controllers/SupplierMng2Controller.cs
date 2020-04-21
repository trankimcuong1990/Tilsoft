using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class SupplierMng2Controller : Controller
    {
        // GET: SupplierMng2
        public ActionResult Index()
        {
            return View();
        }
    }
}