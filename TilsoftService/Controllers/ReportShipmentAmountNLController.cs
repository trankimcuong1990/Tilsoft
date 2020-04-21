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
    [RoutePrefix("api/report_shipmentamount_nl")]
    public class ReportShipmentAmountNLController : ApiController
    {
        private string moduleCode = "ReportShipmentAmountNLMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetShipmentAmountNL(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportShipmentAmount bll = new BLL.ReportShipmentAmount();
            string reportFileName = bll.GetShipmentAmountNL(season,out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getseasons")]
        public IHttpActionResult GetFilters()
        {
            BLL.ReportShipmentAmount bll = new BLL.ReportShipmentAmount();
            Library.DTO.Notification notification;
            IEnumerable<DTO.Support.Season> result = bll.GetSeasons(out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.Season>>() { Data = result, Message = notification, TotalRows = 0 });
        }
    }
}
