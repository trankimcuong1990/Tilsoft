using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.ComponentModel.DataAnnotations;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/backcushion")]
    public class BackCushionController : ApiController
    {
        private string moduleCode = "BackCushionMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BackCushionMng.Executor, Module.BackCushionMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        //[HttpPost]
        //[Route("gets")]
        //public IHttpActionResult Gets(DTO.Search searchInput)
        //{
        //    Library.DTO.Notification notification;
        //    int totalRows = 0;

        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
        //        return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.SearchFormData>() { Data = null, Message = notification, TotalRows = totalRows });
        //    }

        //    BLL.BackCushionMng bll = new BLL.BackCushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
        //    DTO.BackCushionMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
        //    return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        //}
        [HttpPost]
        [Route("gets")]
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

        //[HttpPost]
        //[Route("get")]
        //public IHttpActionResult Get(int id)
        //{
        //    Library.DTO.Notification notification;

        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
        //        return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.EditFormData>() { Data = null, Message = notification });
        //    }

        //    BLL.BackCushionMng bll = new BLL.BackCushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
        //    DTO.BackCushionMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
        //    return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.EditFormData>() { Data = data, Message = notification });
        //}

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;          
            object data = executor.GetData(id,ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //[HttpPost]
        //[Route("update")]
        //public IHttpActionResult Update(int id, DTO.BackCushionMng.BackCushion dtoItem)
        //{
        //    Library.DTO.Notification notification;

        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    bool authenticated = true;
        //    if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
        //    {
        //        // edit case
        //        authenticated = false;
        //    }
        //    else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
        //    {
        //        // create new case       
        //        authenticated = false;
        //    }
        //    if (!authenticated)
        //    {
        //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
        //        return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.BackCushion>() { Data = null, Message = notification });
        //    }

        //    // validation            
        //    if (!Helper.CommonHelper.ValidateDTO<DTO.BackCushionMng.BackCushion>(dtoItem, out notification))
        //    {
        //        return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.BackCushion>() { Data = dtoItem, Message = notification });
        //    }

        //    // continue processing
        //    BLL.BackCushionMng bll = new BLL.BackCushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
        //    bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

        //    return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.BackCushion>() { Data = dtoItem, Message = notification });
        //}

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            BLL.Framework fwBll = new BLL.Framework();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        //[HttpPost]
        //[Route("delete")]
        //public IHttpActionResult Delete(int id)
        //{
        //    Library.DTO.Notification notification;

        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
        //    {
        //        notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
        //        return Ok(new Library.DTO.ReturnData<int>() { Data = -1, Message = notification });
        //    }

        //    BLL.BackCushionMng bll = new BLL.BackCushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
        //    bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
        //    return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        //}

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        //[HttpPost]
        //[Route("getsearchfilter")]
        //public IHttpActionResult GetSearchSupportData()
        //{
        //    BLL.BackCushionMng bll = new BLL.BackCushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
        //    Library.DTO.Notification notification;
        //    DTO.BackCushionMng.SearchFilterData data = bll.GetFilterData(out notification);

        //    return Ok(new Library.DTO.ReturnData<DTO.BackCushionMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        //}

        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
