﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class WeeklyProductionRptController : Controller
    {
        private string ModuleCode = "WeeklyProductionRpt";
        public ActionResult Init()
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}