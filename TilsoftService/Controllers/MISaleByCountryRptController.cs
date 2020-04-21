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
    [RoutePrefix("api/misalebycountry")]
    public class MISaleByCountryRptController : ApiController
    {
        private string moduleCode = "MISaleByCountryRpt";

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

            BLL.MISaleByCountryRpt bll = new BLL.MISaleByCountryRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.MISaleByCountryRpt.ReportData>() { Data = bll.GetReportData(ControllerContext.GetAuthUserId(), season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("getreport-mimargin")]
        public IHttpActionResult GetReportForMiDelta(string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleByCountryRpt bll = new BLL.MISaleByCountryRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.MISaleByCountryRpt.ReportData>() { Data = bll.GetReportForMiDelta(ControllerContext.GetAuthUserId(), season, out notification), Message = notification });
        }
    }
}
