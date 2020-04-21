using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;
namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/reportstockoverview")]
    public class ReportStockOverviewController : ApiController
    {
        private string moduleCode = "ReportStockOverviewMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(Hashtable filters)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportStockOverview bll = new BLL.ReportStockOverview();
            string dataFileName = bll.GetStockOverview(filters,ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getstockoverview")]
        public IHttpActionResult GetStockOverview_HTMLView(Hashtable filters)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportStockOverview bll = new BLL.ReportStockOverview();
            List<DTO.ReportStockOverview.StockOverview> data = bll.GetStockOverview_HTMLView(filters, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ReportStockOverview.StockOverview>>() { Data = data, Message = notification });
        }
    }
}
