using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;
namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/reportStockMutation")]
    public class ReportStockMutationController : ApiController
    {
        private string moduleCode = "ReportStockMutation";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.StockMutationReport.Executor, Module.StockMutationReport");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getReport")]
        public IHttpActionResult GetReportData(Hashtable filters)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getReport", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<object>() { Data = dataFileName, Message = notification });
        }
    }
}
