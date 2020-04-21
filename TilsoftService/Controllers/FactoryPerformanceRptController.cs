using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/factoryperformancerpt")]
    public class FactoryPerformanceRptController : ApiController
    {
        private string moduleCode = "FactoryPerformanceRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryPerformanceRpt.Executor, Module.FactoryPerformanceRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gethtmlreport")]
        public IHttpActionResult GetHTMLReportData(string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "gethtmlreport", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
