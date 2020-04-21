using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ToolController : Controller
    {
        public ActionResult Video()
        {
            return View();
        }
    }
}