﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class TestStandardMngController : Controller
    {
        string moduleCode = "TestStandardMng";
        // GET: TestStandardMng
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-pencil-square-o";
            return View();
        }
    }
}