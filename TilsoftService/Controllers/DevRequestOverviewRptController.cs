using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/devrequestoverviewrpt")]
    public class DevRequestOverviewRptController : ApiController
    {
        private string moduleCode = "DevRequestOverviewRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.DevRequestOverviewRpt.Executor, Module.DevRequestOverviewRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getoverview")]
        public IHttpActionResult GetOverview(int y)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["year"] = y;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getoverview", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getdetail")]
        public IHttpActionResult GetDetail(int y, int u)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["year"] = y;
            param["userId"] = u;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdetail", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }
    }
}
