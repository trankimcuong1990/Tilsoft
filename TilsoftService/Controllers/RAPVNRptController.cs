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
    [RoutePrefix("api/rapvnrpt")]
    public class RAPVNRptController : ApiController
    {
        private string moduleCode = "RAPVNRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.RAPVNRpt.Executor, Module.RAPVNRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            int totalRows = 0;
            input.Filters["totalRows"] = totalRows;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithfilters", input.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification, TotalRows = Convert.ToInt32(input.Filters["totalRows"]) });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(string season, int? factoryID /*string proformaInvoiceNo, string clientUD, string articleCode, string description*/)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["season"] = season;
            filters["factoryID"] = factoryID;
            //filters["proformaInvoiceNo"] = proformaInvoiceNo;
            //filters["clientUD"] = clientUD;
            //filters["articleCode"] = articleCode;
            //filters["description"] = description;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
