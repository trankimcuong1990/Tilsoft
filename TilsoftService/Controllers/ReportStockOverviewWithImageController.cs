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
    [RoutePrefix("api/reportstockoverviewwithimage")]
    public class ReportStockOverviewWithImageController : ApiController
    {
        private string moduleCode = "ReportStockOverviewWithImageMng";

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
            string dataFileName = bll.GetStockOverview(filters,ControllerContext.GetAuthUserId(),out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
