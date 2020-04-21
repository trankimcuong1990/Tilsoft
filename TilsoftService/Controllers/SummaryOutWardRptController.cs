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
    [RoutePrefix("api/summaryoutwardrpt")]
    public class SummaryOutWardRptController : ApiController
    {
        private string moduleCode = "SummaryOutWardRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.SummaryOutWardRpt.Executor, Module.SummaryOutWardRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetDataWithFilters(int? month, int? year, string workOrderStatusNM)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["month"] = month;
            filters["year"] = year;
            filters["workOrderStatusNM"] = workOrderStatusNM;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithfilters", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(int? month, int? year, string workOrderStatusNM)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["month"] = month;
            filters["year"] = year;
            filters["workOrderStatusNM"] = workOrderStatusNM;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
