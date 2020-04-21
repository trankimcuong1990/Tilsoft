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
    [RoutePrefix("api/reportddc")]
    public class ReportDDCController : ApiController
    {
        private string moduleCode = "ReportDDC";

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            BLL.ReportDDC bll = new BLL.ReportDDC();
            Library.DTO.Notification notification;
            DTO.ReportDDC.InitFormData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportDDC.InitFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(string season)
        {
            Library.DTO.Notification notification;
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportDDC bll = new BLL.ReportDDC();
            string dataFileName = bll.GetReportData(ControllerContext.GetAuthUserId(), season, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getreport_html")]
        public IHttpActionResult GetReportData_HTML(string season)
        {
            Library.DTO.Notification notification;
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportDDC bll = new BLL.ReportDDC();
            DTO.DDCReport.ReportData data = bll.GetReportData_HTML(ControllerContext.GetAuthUserId(), season, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.DDCReport.ReportData>() { Data = data, Message = notification });
        }

    }
}
