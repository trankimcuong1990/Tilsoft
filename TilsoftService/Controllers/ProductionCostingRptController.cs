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
    [RoutePrefix("api/productioncostingrpt")]
    public class ProductionCostingRptController : ApiController
    {
        private string moduleCode = "ProductionCostingRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductionCosting.Executor, Module.ProductionCosting");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();          

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetDataWithFilters(int? workOrderID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["workOrderID"] = workOrderID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithfilters", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(int? workOrderID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["workOrderID"] = workOrderID;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
