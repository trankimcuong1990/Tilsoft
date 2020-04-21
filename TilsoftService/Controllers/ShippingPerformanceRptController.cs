using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Library.DTO;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/shipping-performance-rpt")]
    public class ShippingPerformanceRptController : ApiController
    {
        const string moduleCode = "ShippingPerformanceRpt";
        readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ShippingPerformanceRpt.Executor, Module.ShippingPerformanceRpt");
        readonly Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            searchInput.Filters["UserID"] = ControllerContext.GetAuthUserId();
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("export-excel")]
        public IHttpActionResult ExportExcel(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            searchInput.Filters["UserID"] = ControllerContext.GetAuthUserId();
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "export-excel", searchInput.Filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}