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
    [RoutePrefix("api/orderprocessmonitoringrpt")]
    public class OrderProcessMonitoringRptController : ApiController
    {
        private string moduleCode = "OrderProcessMonitoringRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.OrderProcessMonitoringRpt.Executor, Module.OrderProcessMonitoringRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, string t)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["type"] = t;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
