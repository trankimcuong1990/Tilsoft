using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class QualityDocumentMngController : Controller
    {
        string moduleCode = "QualityDocumentMng";
        // GET: QualityDocumentMng
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            ViewBag.Icon = "fa-search";
            return View();
        }
    }
}