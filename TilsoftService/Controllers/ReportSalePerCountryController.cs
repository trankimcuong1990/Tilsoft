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
    [RoutePrefix("api/report_salepercountry")]
    public class ReportSalePerCountryController : ApiController
    {
        private string moduleCode = "ReportSalePerCountryMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(string season, int saleID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportSalePerCountry bll = new BLL.ReportSalePerCountry();
            string reportFileName = bll.GetReportData(season,saleID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getfilters")]
        public IHttpActionResult GetFilters()
        {
            BLL.ReportSalePerCountry bll = new BLL.ReportSalePerCountry();
            Library.DTO.Notification notification;
            DTO.ReportSalePerCountry.SupportDataContainer result = bll.GetFilters(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportSalePerCountry.SupportDataContainer>() { Data = result, Message = notification, TotalRows = 0 });
        }
    }
}
