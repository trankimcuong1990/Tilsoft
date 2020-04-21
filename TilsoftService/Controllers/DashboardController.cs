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
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private string moduleCode = "DashboardMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReport(string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.DashboardMng bll = new BLL.DashboardMng();
            Library.DTO.Notification notification = new Library.DTO.Notification();

            // return dashboard with management information (for management level only)
            return Ok(new Library.DTO.ReturnData<DTO.DashboardMng.DashboardReportData>() { Data = bll.GetReportData(ControllerContext.GetAuthUserId(), season, out notification), Message = notification });
        }
    }
}
