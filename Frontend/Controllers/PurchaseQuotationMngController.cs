﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class PurchaseQuotationMngController : Controller
    {

        string moduleCode = "PurchaseQuotationMng";
        // GET: PurchaseQuotationMng
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        public ActionResult DefaultPrice()
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }
    }
}