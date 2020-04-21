using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/usergroup")]
    public class UserGroupController : ApiController
    {
        private string moduleCode = "UserGroupMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                notification = new Library.DTO.Notification();
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Properties.Resources.NOT_AUTHORIZED;
                return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.SearchFormData>() { Data = null, Message = notification, TotalRows = 0 });
            }

            BLL.UserGroupMng bll = new BLL.UserGroupMng();
            int totalRows = 0;
            DTO.UserGroupMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                notification = new Library.DTO.Notification();
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Properties.Resources.NOT_AUTHORIZED;
                return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.EditFormData>() { Data = null, Message = notification });
            }

            BLL.UserGroupMng bll = new BLL.UserGroupMng();
            DTO.UserGroupMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.UserGroupMng.UserGroup dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                notification = new Library.DTO.Notification();
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Properties.Resources.NOT_AUTHORIZED;
                return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.UserGroup>() { Data = null, Message = notification });
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                notification = new Library.DTO.Notification();
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Properties.Resources.NOT_AUTHORIZED;
                return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.UserGroup>() { Data = null, Message = notification });
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.UserGroupMng.UserGroup>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.UserGroup>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.UserGroupMng bll = new BLL.UserGroupMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.UserGroupMng.UserGroup>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.UserGroupMng bll = new BLL.UserGroupMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }
    }
}
