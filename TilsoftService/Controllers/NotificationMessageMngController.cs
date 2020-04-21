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
    [RoutePrefix("api/notification-message")]
    public class NotificationMessageMngController : ApiController
    {
		private string moduleCode = "NotificationMessageMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.NotificationMessageMng.Executor, Module.NotificationMessageMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("get-message")]
        public IHttpActionResult Get(string key)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // authentication
            int? UserID = fwBll.GetUserIDFromSecreteKey(key, out notification);
            if (UserID.HasValue)
            {
                object data = executor.CustomFunction(UserID.Value, "get-message", new Hashtable(), out notification);
                return Ok(data);
            }
            else
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Invalid key!";
            }

            return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = notification });
        }

		[HttpPost]
        [Route("update-message")]
        public IHttpActionResult Update(string key, object dtoItem)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // authentication
            int? UserID = fwBll.GetUserIDFromSecreteKey(key, out notification);
            if (UserID.HasValue)
            {
                Hashtable param = new Hashtable();
                param["data"] = dtoItem;
                object data = executor.CustomFunction(UserID.Value, "update-message", param, out notification);
                return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
            }
            else
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = "Invalid key!";
            }

            return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = notification });
        }
    }
}
