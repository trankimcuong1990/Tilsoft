﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class FactoryPaymentOverviewRptController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }
    }
}