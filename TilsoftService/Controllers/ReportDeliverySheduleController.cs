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
    [RoutePrefix("api/reportdeliveryshedule")]
    public class ReportDeliverySheduleController : ApiController
    {
        private string moduleCode = "ReportDeliverySheduleMng";

        [HttpPost]
        [Route("getreport")] 
        public IHttpActionResult GetReport(string season, string clientNM, string eta, string containerNo,  int saleID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportDeliveryShedule bll = new BLL.ReportDeliveryShedule();
            string reportFileName = bll.GetReportDeliveryShedule(season, clientNM,
                    eta,
                    containerNo,
                    saleID,
                     ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }
       
        [HttpPost]
        [Route("getfilters")]
        public IHttpActionResult GetFilters()
        {
            BLL.ReportDeliveryShedule bll = new BLL.ReportDeliveryShedule();
            Library.DTO.Notification notification;
            DTO.ReportDeliveryShedule.SupportDataContainer result = bll.GetFilters(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportDeliveryShedule.SupportDataContainer>() { Data = result, Message = notification, TotalRows = 0 });
   
        }
        [HttpPost]
        [Route("print")]
        public IHttpActionResult Print(string ClientNM, string ETA, string ContainerNo, string Season, int SaleID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportDeliveryShedule  bll = new BLL.ReportDeliveryShedule ();
            string reportFileName = bll.GetReportDeliveryShedule (ClientNM, ETA, ContainerNo, Season, SaleID,   ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }
    }
}
