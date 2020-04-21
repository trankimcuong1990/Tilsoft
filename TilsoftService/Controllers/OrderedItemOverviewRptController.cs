using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [System.Web.Http.Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [System.Web.Http.RoutePrefix("api/ordereditemoverview")]

    public class OrderedItemOverviewRptController : ApiController
    {

        private string moduleCode = "OrderedItemOverviewRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.OrderedItemOverviewRpt.Executor, Module.OrderedItemOverviewRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }
        // GET: OrderedItemOverviewRpt
        [HttpPost]
        [Route("getreportdataordered")]
        public IHttpActionResult GetReportData(string s)
        {
          
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable inputs = new Hashtable();

            inputs["season"] = s;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdataordered", inputs, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult Gets(DTO.Search searchInput )
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

    }
}