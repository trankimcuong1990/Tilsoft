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
    [RoutePrefix("api/gallery")]
    public class ImageGalleryController : ApiController
    {
        private string moduleCode = "ImageGalleryMng";

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

            BLL.ImageGalleryMng bll = new BLL.ImageGalleryMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ImageGalleryMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ImageGalleryMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
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

            BLL.ImageGalleryMng bll = new BLL.ImageGalleryMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ImageGalleryMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ImageGalleryMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ImageGalleryMng.ImageGallery dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.ImageGalleryMng.ImageGallery>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.ImageGalleryMng.ImageGallery>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.ImageGalleryMng bll = new BLL.ImageGalleryMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ImageGalleryMng.ImageGallery>() { Data = dtoItem, Message = notification });
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

            BLL.ImageGalleryMng bll = new BLL.ImageGalleryMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchSupportData()
        {
            BLL.ImageGalleryMng bll = new BLL.ImageGalleryMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ImageGalleryMng.SearchFilterData data = bll.GetFilterData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ImageGalleryMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }
    }
}
