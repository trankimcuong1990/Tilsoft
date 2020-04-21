using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class TaskMngController : Controller
    {
        private string moduleCode = "TaskMng";

        public ActionResult Index()
        {            
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult ReadOnly(int id)
        {
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {   
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}