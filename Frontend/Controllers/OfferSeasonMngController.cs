using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
	[Authorize]
    public class OfferSeasonMngController : Controller
    {
        private string ModuleCode = "OfferSeasonMng";
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult _InitForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_InitForm.cshtml");
        }        

        public ActionResult _EditFobProductForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_EditFobProductForm.cshtml");
        }

        public ActionResult _EditFobSparepartForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_EditFobSparepartForm.cshtml");
        }

        public ActionResult _EditWarehouseForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_EditWarehouseForm.cshtml");
        }

        public ActionResult _SearchModelForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_SearchModelForm.cshtml");
        }

        public ActionResult _SearchProductForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_SearchProductForm.cshtml");
        }

        public ActionResult _SearchSparepartForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_SearchSparepartForm.cshtml");
        }

        public ActionResult _OfferItemProperiesForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_OfferItemProperiesForm.cshtml");
        }

        public ActionResult _OfferSparepartProperiesForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_OfferSparepartProperiesForm.cshtml");
        }
        public ActionResult _PlanningPurchasingPriceForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_PlanningPurchasingPriceForm.cshtml");
        }

        public ActionResult _EditFobSampleForm()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/OfferSeasonMng/_Edit/_EditFobSampleForm.cshtml");
        }
    }
}