using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class ClientSpecificationMngController : Controller
    {
        private string moduleCode = "ClientSpecificationMng";

        // GET: ClientSpecificationMng
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        public ActionResult Edit(int id, string code)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.Code = code;
            ViewBag.ModuleCode = moduleCode;

            return View();
        }
    }
}