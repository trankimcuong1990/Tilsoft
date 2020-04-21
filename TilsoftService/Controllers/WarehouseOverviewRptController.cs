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
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/warehouseOverviewRpt")]
    public class WarehouseOverviewRptController : ApiController
    {
        private const string moduleCode = "WarehouseOverviewRpt";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.WarehouseOverviewRpt.Executor, Module.WarehouseOverviewRpt");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(string startDate, string endDate)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();

            input["startDate"] = startDate;
            input["endDate"] = endDate;

            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportwarehouseoverview", input, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
