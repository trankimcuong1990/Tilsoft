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
    [RoutePrefix("api/report_proformainvoice")]
    public class ReportProformaInvoiceController : ApiController
    {
        private string moduleCode = "ReportProformaInvoiceMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportProformaInvoice(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.Report bll = new BLL.Report();
            string reportFileName = bll.GetReportProformaInvoice(season, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getfilters")]
        public IHttpActionResult GetFilters()
        {
            BLL.Report bll = new BLL.Report();
            List<DTO.Support.Season> data = bll.GetSeasons();
            return Ok(new Library.DTO.ReturnData<List<DTO.Support.Season>>() { Data = data, Message = null, TotalRows = 0 });
        }
    }
}
