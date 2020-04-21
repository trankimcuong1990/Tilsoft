using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class QCReportDefaultSettingMngController : Controller
    {
        private string moduleCode = "QCReportDefaultSettingMng";

        // Index
        public ActionResult Index()
        {
            // Set value
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;

            // Return HTML
            return View();
        }

        // Edit
        public ActionResult Edit(int id)
        {
            // Set value
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ID = id;

            // Return HTML
            return View();
        }
    }
}