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
    [RoutePrefix("api/misalemanagement")]
    public class MISaleManagementRptController : ApiController
    {
        private string moduleCode = "MISaleManagementRpt";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReport(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleManagementRpt bll = new BLL.MISaleManagementRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.MISaleManagementRpt.ReportData>() { Data = bll.GetReportData(ControllerContext.GetAuthUserId(), season, saleId, out notification), Message = notification });
        }
        [HttpPost]
        [Route("get-sale-management-for-delta-overview")]
        public IHttpActionResult GetReportData_ForDeltaOverview(string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleManagementRpt bll = new BLL.MISaleManagementRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.MISaleManagementRpt.ReportData>() { Data = bll.GetReportData_ForDeltaOverview(ControllerContext.GetAuthUserId(), season, out notification), Message = notification });
        }
    }
}
