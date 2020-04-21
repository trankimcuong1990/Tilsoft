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
    [RoutePrefix("api/cp-loadingplan")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class CPLoadingPlanController : ApiController
    {
		private string moduleCode = "CPLoadingPlan";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.CPLoadingPlan.Executor, Module.CPLoadingPlan");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

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
            searchInput.Filters["ClientID"] = ControllerContext.GetAuthUserClientID().ToString();
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification;

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
