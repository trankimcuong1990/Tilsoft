﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class WorkOrderController : Controller
    {
        private string moduleCode = "WorkOrderMng";
        // GET: WorkOrder
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