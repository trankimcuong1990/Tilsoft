using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class UnitMngController : Controller
    {

        String moduleCode = "UnitMng";
        // GET: Unit
        public ActionResult Index()
        {
            ViewBag.ModuleCode = moduleCode;
            ViewBag.iCon = "fa-search";
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