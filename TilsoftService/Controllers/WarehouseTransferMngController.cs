using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/WarehouseTransferMng")]
    public class WarehouseTransferMngController : ApiController
    {
        private string moduleCode = "WarehouseTransferMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.WarehouseTransferMng.Executor, Module.WarehouseTransferMng");
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
        public IHttpActionResult Get(int id)
        {
            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            //authentication
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
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("confirm-receiving")]
        public IHttpActionResult ConfirmReceiving(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            object data;

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(new Exception(Properties.Resources.NOT_AUTHORIZED)).Message;

                data = null;
            }
            else
            {
                Hashtable filters = new Hashtable();
                filters["id"] = id;

                data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "confirmreceiving", filters, out notification);
            }

            return Ok(new Library.DTO.ReturnData<object>() { Data=data, Message = notification });
        }

        [HttpPost]
        [Route("confirm-delivering")]
        public IHttpActionResult ConfirmDelivering(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            object data;

            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(new Exception(Properties.Resources.NOT_AUTHORIZED)).Message;

                data = null;
            }
            else
            {
                Hashtable filters = new Hashtable();
                filters["id"] = id;

                data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "confirmdelivering", filters, out notification);
            }

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-production-item")]
        public IHttpActionResult GetProductionItem(string param, string param2, string param3)
        {
            Hashtable filters = new Hashtable();
            filters["searchString"] = param;
            filters["fromBranch"] = param2;
            filters["toBranch"] = param3;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchproductionitem", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getWarehouseTransferPrintoutHTML")]
        public IHttpActionResult GetWarehouseTransferPrintoutHTML(int warehouseTransferID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["warehouseTransferID"] = warehouseTransferID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getWarehouseTransferPrintoutHTML", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getstockqnt-fromwarehouse")]
        public IHttpActionResult GetStockQntFromWarehouse(int factoryWarehouseID, int productionItemID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouseID"] = factoryWarehouseID;
            filters["productionItemID"] = productionItemID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getstockqntfromwarehouse", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}