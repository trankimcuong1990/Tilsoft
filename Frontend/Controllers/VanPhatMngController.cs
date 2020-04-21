using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace Frontend.Controllers
{
	[Authorize]
    public class VanPhatMngController : Controller
    {
        private string ModuleCode = "VanPhatMng";
		public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult PrintOut(string id)
        {
            ViewBag.ID = id;
            ViewBag.Title = "Print out";

            // send request data
            string url = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString() + "api/VanPhatMng/getDeliveryNotePrintoutHTML?deliveryNoteID=" + id.ToString();
            string token = Session[Customize.Common.ProjectDefinition.TOKEN_SESSION].ToString();
            string method = "POST";
            string postedData = string.Empty;
            string jsonToken = "data";
            string returnJsonString = Helper.Common.SendRequest(url, method, postedData, token, jsonToken);
            ViewBag.Data = JObject.Parse(returnJsonString).ToObject<Models.ReportDTO.VPPrintOutDTO>();

            return View();
        }
    }
}