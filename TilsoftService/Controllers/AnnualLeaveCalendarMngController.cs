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
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/AnnualLeaveCalendarMng")]
    public class AnnualLeaveCalendarMngController : ApiController
    {
        private string moduleCode = "AnnualLeaveCalendarMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.AnnualLeaveCalendarMng.Executor, Module.AnnualLeaveCalendarMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

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
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }
        [HttpPost]
        [Route("getsupportdata")]
        public IHttpActionResult GetSupportData()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();          
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsupportdata", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}