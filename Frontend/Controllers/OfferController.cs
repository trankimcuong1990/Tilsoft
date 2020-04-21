using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private string moduleCode = "OfferMng";

        // GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        //public ActionResult Edit(int id)
        //{
        //    ViewBag.Icon = "fa-pencil-square-o";
        //    ViewBag.ID = id;
        //    ViewBag.ModuleCode = moduleCode;
        //    return View();
        //}

        public ActionResult Edit(int id, int id2)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ActionType = id2; // ActionType {0: New, 1: Edit, 2: Copy, 3: NewVersion, }
            ViewBag.ModuleCode = moduleCode;
            ViewBag.CurrentLoginUserInfo = (Frontend.APIDTO.APIUserInformation)Session[Frontend.Customize.Common.ProjectDefinition.USER_INFO_SESSION];            
            return View();
        }

        public ActionResult Overview(int id, int id2)
        {
            ViewBag.Icon = "fa-eye";
            ViewBag.ID = id;
            ViewBag.ActionType = id2;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}