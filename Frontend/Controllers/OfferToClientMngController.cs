using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class OfferToClientMngController : Controller
    {
        private string ModuleCode = "OfferToClientMng";		
        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id, string currency, int clientID, string season)
        {
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            ViewBag.ClientID = clientID;
            ViewBag.Season = season;
            ViewBag.Currency = currency;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }
        public ActionResult _OfferSeasonDetailView()
        {
            ViewBag.ModuleCode = ModuleCode;           
            return PartialView("~/Views/OfferToClientMng/_Edit/_OfferSeasonDetailView.cshtml");
        }

        public ActionResult _General()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferToClientMng/_Edit/_General.cshtml");
        }
        public ActionResult Init()
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }
    }
}