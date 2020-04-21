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
    [RoutePrefix("api/accpioverview")]
    public class AccPIOverviewRptController : ApiController
    {
        private string moduleCode = "AccPIOverviewRpt";

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            BLL.AccPIOverviewRpt bll = new BLL.AccPIOverviewRpt();
            Library.DTO.Notification notification;
            DTO.AccPIOverviewRpt.InitFormData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.AccPIOverviewRpt.InitFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("gethtmlreport")]
        public IHttpActionResult GetHtmlReport(string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.AccPIOverviewRpt bll = new BLL.AccPIOverviewRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.AccPIOverviewRpt.ReportData>() { Data = bll.GetHtmlReportData(ControllerContext.GetAuthUserId(), season, out notification), Message = notification });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.AccPIOverviewRpt bll = new BLL.AccPIOverviewRpt();
            string dataFileName = bll.GetExcelReportData(ControllerContext.GetAuthUserId(), season, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
