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
    [RoutePrefix("api/shipmentToBeInvoicedRpt")]
    public class ShipmentToBeInvoicedRptController : ApiController
    {
        private const string moduleCode = "ShipmentToBeInvoicedRpt";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ShipmentToBeInvoicedRpt.Executor, Module.ShipmentToBeInvoicedRpt");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification;

            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(string season)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable param = new System.Collections.Hashtable();

            param["season"] = season;

            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcelwithseason", param, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
