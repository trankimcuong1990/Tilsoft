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
    [RoutePrefix("api/workorderrpt")]
    public class WorkOrderRptController : ApiController
    {
        private string moduleCode = "WorkOrderRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.WorkOrderRpt.Executor, Module.WorkOrderRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdata", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
