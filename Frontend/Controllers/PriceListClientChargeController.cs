namespace Frontend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class PriceListClientChargeController : Controller
    {
        #region ** Variable & Constant **

        string moduleCode = "PriceListClientCharge";

        #endregion

        #region ** Constructor **

        #endregion

        #region ** Function & Method **

        // GET: PriceListClientCharge
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;

            return View();
        }

        #endregion
    }
}