using Library.Base;
using Library.DTO;
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
    [RoutePrefix("api/notification")]
    public class NotificationController : ApiController
    {
        private string moduleCode = "NotificationMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.NotificationMng.Executor, Module.NotificationMng");
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
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object obj)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref obj, out Notification notification);
            return Ok(new ReturnData<object>() { Data = obj, Message = notification });
        }

        [HttpPost]
        [Route("getsearchuser")]
        public IHttpActionResult GetUserFilter(DTO.Search filters)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getuserfilter", filters.Filters, out Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getModuleStatus")]
        public IHttpActionResult GetModuleStatus(int moduleID, int notificationGroupID)
        {
            Hashtable filters = new Hashtable() { ["moduleID"] = moduleID, ["notificationGroupID"] = notificationGroupID };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "load-module-status", filters, out Library.DTO.Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("saveModuleStatus")]
        public IHttpActionResult UpdateModuleStatus(int id, int moduleID, string statusUD, string statusNM)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            Hashtable filters = new Hashtable() { ["id"] = id, ["moduleID"] = moduleID, ["statusUD"] = statusUD, ["statusNM"] = statusNM };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-module-status", filters, out Library.DTO.Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}