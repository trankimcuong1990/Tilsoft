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
    [RoutePrefix("api/offerreportrpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class OfferReportRptController : ApiController
    {
		private string moduleCode = "OfferReportRpt";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.OfferReportRpt.Executor, Module.OfferReportRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getuserfobitemonly")]
        public IHttpActionResult GetUserFOBItemOnly(string season)
        {
            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["season"] = season
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getuserfobitemonly", inputs, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("excel-fobitemonly")]
        public IHttpActionResult GetExcelFOBItemOnlyReportData(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["season"] = season
            };

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "excel-fobitemonly", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
