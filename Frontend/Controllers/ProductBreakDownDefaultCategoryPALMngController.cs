﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class ProductBreakDownDefaultCategoryPALMngController : Controller
    {
        private readonly string moduleCode = "ProductBreakDownDefaultPALMng";

		public ActionResult Init()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;
            return View();
        }
    }
}