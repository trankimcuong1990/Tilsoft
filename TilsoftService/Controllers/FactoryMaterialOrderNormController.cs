﻿using System;
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
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/factoryMaterialOrderNorm")]
    public class FactoryMaterialOrderNormController : ApiController
    {
        private string moduleCode = "FactoryMaterialOrderNormMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryMaterialOrderNorm.Executor, Module.FactoryMaterialOrderNorm");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification , TotalRows = totalRows});
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
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
            Library.DTO.Notification notification;
            //set indenfier is tempFolder which to use to save image
            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            //update data
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

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

        [HttpPost]
        [Route("searchclient")]
        public IHttpActionResult GetListClient(DTO.Search searchInput)
        {
            //get input
            Hashtable filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetListClient", filters, out notification);
            
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification});
        }

        [HttpPost]
        [Route("geteditformdata")]
        public IHttpActionResult GetEditFormData(int id, int? clientID, int? productID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //get input
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["clientID"] = clientID;
            filters["productID"] = productID;

            //get data
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetEditFormData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("createordernorm")]
        public IHttpActionResult CreateOrderNorm(object dtoOrder)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["dtoOrder"] = dtoOrder;

            //get data
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateOrderNorm", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    
    }
}