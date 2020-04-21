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
    [RoutePrefix("api/permissionoverviewrpt")]
    public class PermissionOverviewRptController : ApiController
    {
        private string moduleCode = "PermissionOverviewRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PermissionOverviewRpt.Executor, Module.PermissionOverviewRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(int? m, int? u, int? ug, int? o)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            if (m.HasValue)
            {
                inputs["moduleID"] = m.Value;
            }
            if (u.HasValue)
            {
                inputs["userID"] = u.Value;
            }
            if (ug.HasValue)
            {
                inputs["userGroupID"] = ug.Value;
            }
            if (o.HasValue)
            {
                inputs["officeID"] = o.Value;
            }
            return Ok(executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdata", inputs, out notification));
        }

        [HttpPost]
        [Route("getreportdatadetail")]
        public IHttpActionResult GetReportDataDetail(int? m, int? u, int? ug, int? o)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            if (m.HasValue)
            {
                inputs["moduleID"] = m.Value;
            }
            if (u.HasValue)
            {
                inputs["userID"] = u.Value;
            }
            if (ug.HasValue)
            {
                inputs["userGroupID"] = ug.Value;
            }
            if (o.HasValue)
            {
                inputs["officeID"] = o.Value;
            }
            return Ok(executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdatadetail", inputs, out notification));
        }
    }
}
