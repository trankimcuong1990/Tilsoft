using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class PurchaseOrderMngController : Controller
    {
        private string moduleCode = "PurchaseOrderMng";
        // GET: PurchaseOrderMng
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            ViewBag.from = Request.QueryString["purchaseOrderIDs"];
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Icon = (id == 0) ? "fa-file-o" : "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
        public ActionResult PrintOut(int id)
        {
            ViewBag.ID = id;
            ViewBag.Title = "Print out";

            // send request data
            string url = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString() + "api/purchaseOrderMng/gethtmlreport?purchaseOrderID=" + id.ToString();
            string token = Session[Customize.Common.ProjectDefinition.TOKEN_SESSION].ToString();
            string method = "POST";
            string postedData = string.Empty;
            string jsonToken = "data";
            string returnJsonString = Helper.Common.SendRequest(url, method, postedData, token, jsonToken);
            ViewBag.Data = JObject.Parse(returnJsonString).ToObject<Models.ReportDTO.PurchaseOrderPrintOutDTO>();

            return View();
        }
    }
}