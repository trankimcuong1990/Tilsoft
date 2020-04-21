using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/stockstatusqnt-report")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class StockStatusQntRptController : ApiController
    {
        private readonly string moduleCode = "StockStatusQntRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.StockStatusQntRpt.Executor, Module.StockStatusQntRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData(int branchID)
        {
            System.Collections.Hashtable param = new System.Collections.Hashtable();
            param["branchID"] = branchID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinitdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

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

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }
    }
}