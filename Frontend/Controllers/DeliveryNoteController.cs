﻿namespace Frontend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DeliveryNoteController : Controller
    {
        private string moduleCode = "DeliveryNote";

        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = this.moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = this.moduleCode;
            return View();
        }
    }
}