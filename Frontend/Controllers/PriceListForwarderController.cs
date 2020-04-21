namespace Frontend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class PriceListForwarderController : Controller
    {
        #region ** Variables & Constants **

        private string moduleCode = "PriceListForwarder";

        #endregion

        #region ** Methods & Functions **

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