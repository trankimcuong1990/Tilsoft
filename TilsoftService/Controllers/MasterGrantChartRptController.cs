using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/mastergrantchartrpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class MasterGrantChartRptController : ApiController
    {
		private string moduleCode = "MasterGrantChartRpt";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.MasterGrantChartRpt.Executor, Module.MasterGrantChartRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            searchInput.Filters["UserID"] = ControllerContext.GetAuthUserId();
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get-work-order-production-detail")]
        public IHttpActionResult GetWorkOrderProductionDetail(int workOrderID)
        {
            Library.DTO.Notification notification;
            Hashtable input = new Hashtable();
            input["workOrderID"] = workOrderID;
            Object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-work-order-production-detail", input , out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification});
        }
    }
}
