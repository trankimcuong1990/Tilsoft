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
    [RoutePrefix("api/factorywarehouse")]
    public class FactoryWarehouseController : ApiController
    {
        private string moduleCode = "FactoryWarehouse";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryWarehouse.Executor, Module.FactoryWarehouse");
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

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, object param)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            input["BranchID"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

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

        //
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

        [HttpPost]
        [Route("getCapacityData")]
        public IHttpActionResult GetCapacityData(int id)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getCapacityData", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getCapacityDetailData")]
        public IHttpActionResult GetCapacityDetailData(int id)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getCapacityDetailData", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
