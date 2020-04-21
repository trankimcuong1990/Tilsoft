using Library.DTO;
using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/workOrder")]
    public class WorkOrderController : ApiController
    {
        private string moduleCode = "WorkOrderMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.WorkOrder.Executor, Module.WorkOrder");
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
        public IHttpActionResult Get(int id, int branchID)
        {
            Library.DTO.Notification notification = new Notification();
            notification.Type = NotificationType.Success;

            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["branchID"] = branchID;
            //object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
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
        [Route("approve")]
        public IHttpActionResult Confirm(int id, object dtoItem)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification;
            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getProduct")]
        public IHttpActionResult GetProduct(DTO.Search searchInput)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProduct", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSaleOrder")]
        public IHttpActionResult GetSaleOrder(DTO.Search searchInput)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetSaleOrder", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFactoryOrderDetail")]
        public IHttpActionResult GetFactoryOrderDetail(DTO.Search searchInput)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactoryOrderDetail", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("setWorkOrderStatus")]
        public IHttpActionResult SetWorkOrderStatus(int? workOrderID, int? workOrderStatusID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable param = new System.Collections.Hashtable();
            param["workOrderID"] = workOrderID;
            param["workOrderStatusID"] = workOrderStatusID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "SetWorkOrderStatus", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("change-opsequence")]
        public IHttpActionResult ChangeOPSequence(System.Collections.Hashtable param)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ChangeOPSequence", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-sample-order")]
        public IHttpActionResult GetSampleOrder(System.Collections.Hashtable filters)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetSampleOrder", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-pre-order-work-order")]
        public IHttpActionResult GetPreOrderWorkOrder(string workOrderUD)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Hashtable input = new Hashtable();
            input["workOrderUD"] = workOrderUD;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPreOrderWorkOrder", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getworkcenter")]
        public IHttpActionResult GetWorkCenter()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkCenter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexcelworkorder")]
        public IHttpActionResult GetExcelWorkOrder(string workOrders, string workCenters)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Hashtable input = new Hashtable();
            input["workOrders"] = workOrders;
            input["workCenters"] = workCenters;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetExcelWorkOrder", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("openworkorderstatus")]
        public IHttpActionResult OpenWorkOrderStatus(int? workOrderID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable param = new System.Collections.Hashtable();
            param["workOrderID"] = workOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "OpenWorkOrderStatus", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-opsequencedetail")]
        public IHttpActionResult GetOPSequenceDetails(int opSequenceID, int? preWorkOrderID)
        {
            Hashtable filters = new Hashtable();
            filters["OPSequenceID"] = opSequenceID;
            filters["PreWorkOrderID"] = preWorkOrderID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetOPSequenceDetails", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPreOrderWorkOrder")]
        public IHttpActionResult GetPreOrderWorkOrderInformation(int preOrderWorkOrderID)
        {
            Hashtable filters = new Hashtable();
            filters["PreOrderWorkOrderID"] = preOrderWorkOrderID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPreOrderWorkOrderManagement", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getWorkOrderBaseOn")]
        public IHttpActionResult GetWorkOrderBaseOnManagement(int workOrderID, int preOrderWorkOrderID)
        {
            Hashtable filters = new Hashtable();
            filters["WorkOrderID"] = workOrderID;
            filters["PreOrderWorkOrderID"] = preOrderWorkOrderID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkOrderBaseOnManagement", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateTransferWorkOrder")]
        public IHttpActionResult UpdateTransferWorkOrder(int transferWorkOrderID, object transferWorkOrder)
        {
            if (transferWorkOrderID == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (transferWorkOrderID > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["TransferWorkOrder"] = transferWorkOrder;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateTransferPre2Work", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getHistoryTransferPreOrder")]
        public IHttpActionResult GetHistoryTransferPreOrderToWorkOrder(int workOrderID, int preOrderWorkOrderID)
        {
            Hashtable filters = new Hashtable();
            filters["WorkOrderID"] = workOrderID;
            filters["PreOrderWorkOrderID"] = preOrderWorkOrderID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetHistoryTransferPreOrderToWorkOrder", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getTransferWorkOrder")]
        public IHttpActionResult GetTransferWorkOrder(int id, int workOrderID, int preOrderWorkOrderID)
        {
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["workOrderID"] = workOrderID;
            filters["preOrderWorkOrderID"] = preOrderWorkOrderID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetTransferWorkOrder", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
