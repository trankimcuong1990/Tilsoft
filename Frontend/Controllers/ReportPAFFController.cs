﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ReportPAFFController : Controller
    {
        private string moduleCode = "ReportPAFFMng";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
	}
}