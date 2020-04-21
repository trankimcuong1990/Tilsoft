using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class DeliveryNote2Controller : Controller
    {
        private string moduleCode = "DeliveryNote2Mng";

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

        public ActionResult Warehouse2Team(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Team2Team(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult AmendWarehouse2Team(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult AmendTeam2Team(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult SaleOrder(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult SaleOrderWithoutWorkOrder(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
        public ActionResult PrintOut(int id)
        {
            ViewBag.ID = id;
            ViewBag.Title = "Print out";

            // send request data
            string url = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString() + "api/deliveryNote2/getDeliveryNotePrintoutHTML?deliveryNoteID=" + id.ToString();
            string token = Session[Customize.Common.ProjectDefinition.TOKEN_SESSION].ToString();
            string method = "POST";
            string postedData = string.Empty;
            string jsonToken = "data";
            string returnJsonString = Helper.Common.SendRequest(url, method, postedData, token, jsonToken);
            ViewBag.Data = JObject.Parse(returnJsonString).ToObject<Models.ReportDTO.DeliveryNotePrintoutDTO>();

            return View();
        }

        public ActionResult Warehouse2TeamOverView(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult SaleOrderWithoutWorkOrderOverview (int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}