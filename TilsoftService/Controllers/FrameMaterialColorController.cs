
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
    [RoutePrefix("api/framematerialcolor")]
    public class FrameMaterialColorController : ApiController
    {
        private string moduleCode = "FrameMaterialColorMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FrameMaterialColorMng.Executor, Module.FrameMaterialColorMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();


        [HttpPost]
        [Route("gets")]
        //public IHttpActionResult Gets(DTO.Search searchInput)
        //{
        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    BLL.FrameMaterialColorMng bll = new BLL.FrameMaterialColorMng();
        //    Library.DTO.Notification notification;
        //    int totalRows = 0;
        //    DTO.FrameMaterialColorMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
        //    if (notification.Type == Library.DTO.NotificationType.Error)
        //    {
        //        return InternalServerError(new Exception(notification.Message));
        //    }
        //    return Ok(new Library.DTO.ReturnData<DTO.FrameMaterialColorMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        //}
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }
        [HttpPost]
        [Route("get")]
        //public IHttpActionResult Get(int id)
        //{
        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    BLL.FrameMaterialColorMng bll = new BLL.FrameMaterialColorMng();
        //    Library.DTO.Notification notification;
        //    DTO.FrameMaterialColorMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
        //    if (notification.Type == Library.DTO.NotificationType.Error)
        //    {
        //        return InternalServerError(new Exception(notification.Message));
        //    }
        //    return Ok(new Library.DTO.ReturnData<DTO.FrameMaterialColorMng.EditFormData>() { Data = data, Message = notification });
        //}

        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;           
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("update")]
        //public IHttpActionResult Update(int id, DTO.FrameMaterialColorMng.FrameMaterialColor dtoItem)
        //{
        //    Library.DTO.Notification notification;

        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
        //    {
        //        // edit case
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }
        //    else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
        //    {
        //        // create new case
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    // validation            
        //    if (!Helper.CommonHelper.ValidateDTO<DTO.FrameMaterialColorMng.FrameMaterialColor>(dtoItem, out notification))
        //    {
        //        return Ok(new Library.DTO.ReturnData<DTO.FrameMaterialColorMng.FrameMaterialColor>() { Data = dtoItem, Message = notification });
        //    }

        //    // continue processing
        //    BLL.FrameMaterialColorMng bll = new BLL.FrameMaterialColorMng();
        //    bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
        //    if (notification.Type == Library.DTO.NotificationType.Error)
        //    {
        //        return InternalServerError(new Exception(notification.Message));
        //    }
        //    return Ok(new Library.DTO.ReturnData<DTO.FrameMaterialColorMng.FrameMaterialColor>() { Data = dtoItem, Message = notification });
        //}

        public IHttpActionResult Update(int id, object dtoItem)
        {
           
            Library.DTO.Notification notification;
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            // continue processing
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        //public IHttpActionResult Delete(int id)
        //{
        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    BLL.FrameMaterialColorMng bll = new BLL.FrameMaterialColorMng();
        //    Library.DTO.Notification notification;
        //    bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
        //    if (notification.Type == Library.DTO.NotificationType.Error)
        //    {
        //        return InternalServerError(new Exception(notification.Message));
        //    }
        //    return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        //}
        public IHttpActionResult Delete(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }
    }
}
