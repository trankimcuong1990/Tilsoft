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
    [RoutePrefix("api/report_paff")]
    public class ReportPAFFController : ApiController
    {
        private string moduleCode = "ReportPAFFMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportPAFF(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportPAFF bll = new BLL.ReportPAFF();
            string reportFileName = bll.GetReportPAFF(season, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getseasons")]
        public IHttpActionResult GetSeasons()
        {
            BLL.ReportPAFF bll = new BLL.ReportPAFF();
            List<DTO.Support.Season> seasons = bll.GetSeasons();
            return Ok(new Library.DTO.ReturnData<List<DTO.Support.Season>>() { Data = seasons });
        }
    }
}
