using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/transport-rpt")]
    public class TransportRptController : ApiController
    {
        private string moduleCode = "TransportRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.TransportRpt.Executor, Module.TransportRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("get-report-data")]
        public IHttpActionResult GetReportData()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetReportData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
