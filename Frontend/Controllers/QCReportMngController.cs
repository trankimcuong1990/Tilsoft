using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class QCReportMngController : Controller
    {
        private string moduleCode = "QCReportMng";
        // GET: QCReportMng
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id, int? saleOrderDetailID, int? factoryID, string itemFactoryOrderLink, int? clientID, string articleCode)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.SaleOrderDetailID = saleOrderDetailID;
            ViewBag.FactoryID = factoryID;
            ViewBag.ModuleCode = moduleCode;
            ViewBag.ItemFactoryOrderLink = itemFactoryOrderLink;
            ViewBag.ClientID = clientID;
            ViewBag.ArticleCode = articleCode;
            return View();
        }

        public ActionResult Init()
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

    }
}