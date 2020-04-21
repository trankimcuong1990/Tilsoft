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
    [RoutePrefix("api/mioverviewrpt")]
    public class MIOverviewRptController : ApiController
    {
        private string moduleCode = "MIOverviewRpt";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReport(string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MIOverviewRpt bll = new BLL.MIOverviewRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.MIOverviewRpt.ReportData>() { Data = bll.GetReportData(ControllerContext.GetAuthUserId(), season, out notification), Message = notification });
        }
    }
}
