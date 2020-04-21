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
    [RoutePrefix("api/cushioncolor")]
    public class CushionColorController : ApiController
    {
        private string moduleCode = "CushionColorMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;
            int totalRows = 0;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
                return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.SearchFormData>() { Data = null, Message = notification, TotalRows = totalRows });
            }

            BLL.CushionColorMng bll = new BLL.CushionColorMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            DTO.CushionColorMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
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
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
                return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.EditFormData>() { Data = null, Message = notification });
            }

            BLL.CushionColorMng bll = new BLL.CushionColorMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            DTO.CushionColorMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.CushionColorMng.CushionColor dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            bool authenticated = true;
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                authenticated = false;
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case       
                authenticated = false;
            }
            if (!authenticated)
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
                return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.CushionColor>() { Data = null, Message = notification });
            }

            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.CushionColorMng.CushionColor>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.CushionColor>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.CushionColorMng bll = new BLL.CushionColorMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.CushionColor>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED };
                return Ok(new Library.DTO.ReturnData<int>() { Data = -1, Message = notification });
            }

            BLL.CushionColorMng bll = new BLL.CushionColorMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchSupportData()
        {
            BLL.CushionColorMng bll = new BLL.CushionColorMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.CushionColorMng.SearchFilterData data = bll.GetFilterData(out notification);

            return Ok(new Library.DTO.ReturnData<DTO.CushionColorMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }
    }
}