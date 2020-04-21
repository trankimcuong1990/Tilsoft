using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class LoadingPlanController : Controller
    {
        const string ModuleCode = "LoadingPlanMng";

        // GET: LoadingPlanningok
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = ModuleCode;

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Init()
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Overview(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult PrintOutHTML(int id)
        {
            ViewBag.ID = id;
            ViewBag.Title = "LoadingPlan HTML PrintOut";

            //Sent request data
            string url = System.Configuration.ConfigurationManager.AppSettings["BackendServiceUrl"].ToString() + "api/loadingplan/getPrintOutHTML?loadingPlanID=" + id.ToString();
            string token = Session[Customize.Common.ProjectDefinition.TOKEN_SESSION].ToString();
            string method = "POST";
            string postedData = string.Empty;
            string jsonToken = "data";
            string returnJsonString = Helper.Common.SendRequest(url, method, postedData, token, jsonToken);
            ViewBag.Data = JObject.Parse(returnJsonString).ToObject<Frontend.Models.ReportDTO.LoadingPlanReportDTO>();

            return View();
        }
    }
}