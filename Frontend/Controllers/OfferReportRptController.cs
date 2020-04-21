﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class OfferReportRptController : Controller
    {
        private string ModuleCode = "OfferReportRpt";
		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}