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
    [RoutePrefix("api/reportfreetosaleoverview")]
    public class ReportFreeToSaleOverviewController : ApiController
    {
        private string moduleCode = "ReportFreeToSaleOverviewMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportFreeToSaleOverview bll = new BLL.ReportFreeToSaleOverview();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ReportFreeToSale.SearchFormData data = bll.GetFreeToSaleSearch(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportFreeToSale.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }


        [HttpPost]
        [Route("getfreetosaleoverview")]
        public IHttpActionResult GetReportData(bool isIncludeImage)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportFreeToSaleOverview bll = new BLL.ReportFreeToSaleOverview();
            string dataFileName = bll.GetFreeToSaleOverview(isIncludeImage, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getfreetosaleselected")]
        public IHttpActionResult GetReportData(Hashtable filters)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportFreeToSaleOverview bll = new BLL.ReportFreeToSaleOverview();
            string dataFileName = bll.GetFreeToSaleSelected(filters, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }



    }
}
