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
    [RoutePrefix("api/misalebyitem")]
    public class MISaleByItemRptController : ApiController
    {
        private string moduleCode = "MISaleByItemRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.MISaleByItemRpt.Executor, Module.MISaleByItemRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReport(string season)
        {
            // authentication            
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["Season"] = season;
            Object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdata", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getreportdata-for-delta-overview")]
        public IHttpActionResult GetReportData_ForDeltaOverview(string season)
        {
            // authentication            
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["Season"] = season;
            Object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdata-for-delta-overview", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-piconfirmed-by-itemcategory")]
        public IHttpActionResult GetPiConfirmedByItemCategory(string season)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["Season"] = season;
            Object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-piconfirmed-by-itemcategory", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }
    }
}
