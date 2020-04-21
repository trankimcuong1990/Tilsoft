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
    [RoutePrefix("api/bizbloqs")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class BizBloqSyncMngController : ApiController
    {
		private string moduleCode = "BizBloqSyncMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BizBloqSyncMng.Executor, Module.BizBloqSyncMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

		[HttpPost]
        [Route("sync-from-bizbloqs")]
        public IHttpActionResult SynzFromBizBloq(object jsonData)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED } });
            }
            else if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED } });
            }
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Hashtable param = new Hashtable();
            param["data"] = jsonData;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "sync-from-bizbloqs", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
