﻿using System;
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
    [RoutePrefix("api/cushion")]
    public class CushionController : ApiController
    {
        private string moduleCode = "CushionMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.CushionMng bll = new BLL.CushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.CushionMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.CushionMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.CushionMng bll = new BLL.CushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.CushionMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.CushionMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.CushionMng.Cushion dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
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

            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.CushionMng.Cushion>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.CushionMng.Cushion>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.CushionMng bll = new BLL.CushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.CushionMng.Cushion>() { Data = dtoItem, Message = notification });
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

            BLL.CushionMng bll = new BLL.CushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchSupportData()
        {
            BLL.CushionMng bll = new BLL.CushionMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.CushionMng.SearchFilterData data = bll.GetFilterData(out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.CushionMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }
    }
}