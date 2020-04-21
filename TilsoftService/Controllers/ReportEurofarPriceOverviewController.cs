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
    [RoutePrefix("api/eurofarpriceoverviewrpt")]
    public class ReportEurofarPriceOverviewController : ApiController
    {
        private string moduleCode = "ReportEurofarPriceOverview";

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            BLL.EurofarPriceOverviewRpt bll = new BLL.EurofarPriceOverviewRpt();
            Library.DTO.Notification notification;
            DTO.EurofarPriceOverViewRpt.InitFormData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.EurofarPriceOverViewRpt.InitFormData>() { Data = data, Message = notification, TotalRows = 0 });
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

            BLL.EurofarPriceOverviewRpt bll = new BLL.EurofarPriceOverviewRpt();
            string dataFileName = bll.GetReportData(ControllerContext.GetAuthUserId(), season, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return Ok(new Library.DTO.ReturnData<string>() { Data = string.Empty, Message = notification });
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
