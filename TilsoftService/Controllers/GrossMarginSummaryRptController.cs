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
    [RoutePrefix("api/grossmarginsummary")]
    public class GrossMarginSummaryRptController : ApiController
    {
        private string moduleCode = "GrossMarginSummaryRpt";

        [HttpPost]
        [Route("gethtmlreport")]
        public IHttpActionResult GetHtmlReport(string f, string t)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.GrossMarginSummaryRpt bll = new BLL.GrossMarginSummaryRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.GrossMarginSummaryRpt.ReportData>() { Data = bll.GetHtmlReportData(ControllerContext.GetAuthUserId(), f, t, out notification), Message = notification });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(string f, string t)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.GrossMarginSummaryRpt bll = new BLL.GrossMarginSummaryRpt();
            string dataFileName = bll.GetExcelReportData(ControllerContext.GetAuthUserId(), f, t, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getexcelforecastreport")]
        public IHttpActionResult GetExcelForecastReportData(string s)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (string.IsNullOrEmpty(s)) return InternalServerError(new Exception("Invalid season!"));

            BLL.GrossMarginSummaryRpt bll = new BLL.GrossMarginSummaryRpt();
            string dataFileName = bll.GetExcelForecastReportData(ControllerContext.GetAuthUserId(), s, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
