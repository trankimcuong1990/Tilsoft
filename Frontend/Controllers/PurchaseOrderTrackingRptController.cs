﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class PurchaseOrderTrackingRptController : Controller
    {
        private string ModuleCode = "PurchaseOrderTrackingRpt";

        public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.Icon = "fa-file-excel-o";
            ViewBag.Title = "Purchase Order Tracking";

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