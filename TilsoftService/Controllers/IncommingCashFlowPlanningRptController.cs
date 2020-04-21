using System;
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
    [RoutePrefix("api/incommingcashflowplanningrpt")]
    public class IncommingCashFlowPlanningRptController : ApiController
    {
        private string moduleCode = "IncommingCashFlowPlanningRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.IncommingCashFlowPlanningRpt.Executor, Module.IncommingCashFlowPlanningRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getexcelreportdata")]
        public IHttpActionResult GetExcelReportData(string season)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["Season"] = season;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreportdata", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
