﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class MIMarginOverviewRptController : Controller
    {
        private string ModuleCode = "MIMarginOverviewRpt";

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}