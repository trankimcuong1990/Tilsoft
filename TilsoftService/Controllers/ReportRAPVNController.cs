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
    [RoutePrefix("api/report_rapvn")]
    public class ReportRAPVNController : ApiController
    {
        private string moduleCode = "ReportRAPVNMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportRAPEU(string season, int factoryID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportRAPEU bll = new BLL.ReportRAPEU();
            string reportFileName = bll.GetReportRAPEU(false,season,factoryID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getfilters")]
        public IHttpActionResult GetFilters()
        {
            BLL.ReportRAPEU bll = new BLL.ReportRAPEU();
            Library.DTO.Notification notification;
            DTO.ReportRAPEU.SupportDataContainer result = bll.GetFilters(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportRAPEU.SupportDataContainer>() { Data = result, Message = notification, TotalRows = 0 });
        }
    }
}
