using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/FactoryProductionStatus")]
    public class FactoryProductionStatusController : ApiController
    {
        private string moduleCode = "FactoryProductionStatusMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryProductionStatus.Executor, Module.FactoryProductionStatus");
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
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int factoryProductionStatusID, int? factoryID, string season)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryProductionStatusID"] = factoryProductionStatusID;
            filters["factoryID"] = factoryID;
            filters["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
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

        [HttpPost]
        [Route("getProductionOverview")]
        public IHttpActionResult GetProductionOverview(int? factoryID, string season, bool? isGetSupportData)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryID"] = factoryID;
            filters["season"] = season;
            filters["isGetSupportData"] = isGetSupportData;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionOverview", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getOrderDetail")]
        public IHttpActionResult GetOrderDetail(int? factoryOrderID, string excludeFactoryOrderDetailIDs)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryOrderID"] = factoryOrderID;
            filters["excludeFactoryOrderDetailIDs"] = excludeFactoryOrderDetailIDs;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetOrderDetail", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }





    }
}
