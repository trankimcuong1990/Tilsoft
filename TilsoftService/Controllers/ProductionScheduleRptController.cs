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
    [RoutePrefix("api/ProductionScheduleRpt")]
    public class ProductionScheduleRptController : ApiController
    {
        private string moduleCode = "ProductionScheduleRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductionScheduleRpt.Executor, Module.ProductionScheduleRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionSchedule", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("finishComponent")]
        public IHttpActionResult FinishComponent(int receiptDeptID, object finishComponentData)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["receiptDeptID"] = receiptDeptID;
            filters["finishComponentData"] = finishComponentData;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "FinishComponent", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}