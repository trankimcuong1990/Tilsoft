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
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/productionIssueMng")]
    public class ProductionIssueRptController : ApiController
    {
        private const string moduleCode = "ProductionIssueMng";

        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductionIssue.Executor, Module.ProductionIssue");
        private readonly Module.Framework.BLL frameworkBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInit()
        {
            Library.DTO.Notification notification;

            if (!frameworkBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            int totalRows;
            Library.DTO.Notification notification;

            if (!frameworkBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            IHttpActionResult httpActionResult;
            Library.DTO.Notification notification;

            if (HasNotUpdatePermission(id, out httpActionResult))
            {
                return httpActionResult;
            }

            SetModuleIdenfitier(executor);

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getWorkOrder")]
        public IHttpActionResult GetWorkOrder(string param)
        {
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkOrder", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getHistoryDeliveryNote")]
        public IHttpActionResult GetHistoryDeliveryNote(int workOrderID, int workCenterID, int productionItemID)
        {
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["workOrderID"] = workOrderID;
            input["workCenterID"] = workCenterID;
            input["productionItemID"] = productionItemID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetHistoryDeliveryNote", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        private void SetModuleIdenfitier(Library.Base.IExecutor executor)
        {
            if (executor != null)
            {
                executor.identifier = ControllerContext.GetCurrentUserFolder();
            }
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id == 0 && !frameworkBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !frameworkBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }
    }
}
