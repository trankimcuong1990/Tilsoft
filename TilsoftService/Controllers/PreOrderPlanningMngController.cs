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
    [RoutePrefix("api/preOrderPlanningMng")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class PreOrderPlanningMngController : ApiController
    {
		private string moduleCode = "PreOrderPlanningMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PreOrderPlanningMng.Executor, Module.PreOrderPlanningMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
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
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

		[HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, string season, int factoryID, object dtoItem)
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

            Hashtable filters = new Hashtable();
            filters["season"] = season;
            filters["factoryID"] = factoryID;
            filters["dataView"] = dtoItem;

            executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatedata", filters, out Library.DTO.Notification notification);

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
            object data = executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            executor.Reset(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }
    }
}
