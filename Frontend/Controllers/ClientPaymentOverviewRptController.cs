﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ClientPaymentOverviewRptController : Controller
    {
        // GET: ClientPaymentOverviewRpt
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-bar-chart-o";
            return View();
        }
    }
}