﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class PODController : Controller
    {
        private string moduleCode = "PODMng";

        // GET: POD
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}