using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/ReceivingNote")]
    public class ReceivingNoteController : ApiController
    {
        private string moduleCode = "ReceivingNoteMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ReceivingNote.Executor, Module.ReceivingNote");
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
        public IHttpActionResult Get(int id, int? workOrderID, int? opSequenceID, int? branchID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["workOrderID"] = workOrderID;
            filters["opSequenceID"] = opSequenceID;
            filters["branchID"] = branchID;
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
        [Route("getReceivingNotePrintout")]
        public IHttpActionResult GetReceivingNotePrintout(int receivingNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["receivingNoteID"] = receivingNoteID;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetReceivingNotePrintout", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, string workOrderIDs, int? branchID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();

            filters["id"] = id;
            filters["workOrderIDs"] = workOrderIDs;
            filters["branchID"] = branchID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getReceivingNotePrintoutHTML")]
        public IHttpActionResult GetReceivingNotePrintoutHTML(int receivingNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["receivingNoteID"] = receivingNoteID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetReceivingNotePrintoutHTML", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getReceivingNoteVanPhatPrintoutHTML")]
        public IHttpActionResult GetReceivingNoteVanPhatPrintoutHTML(int receivingNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["receivingNoteID"] = receivingNoteID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetReceivingNotePrintoutHTML", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getsubsupplier")]
        public IHttpActionResult GetSubSupplier(string param)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsubsupplier", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteWithPermission")]
        public IHttpActionResult DeactiveAccount(int id, int? createdBy)
        {
            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable
            {
                ["id"] = id,
                ["createdBy"] = createdBy
            };
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteWithPermission", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("get-purchase-order")]
        public IHttpActionResult GetPurchaseOrder(string param)
        {
            Hashtable filters = new Hashtable();
            filters["purchaseOrderUD"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPurchaseOrder", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("get-production-item")]
        public IHttpActionResult GetProductionItem(string param, string param2 = null, string param3 = null, string param4 = null, string param5 = null)
        {
            Hashtable filter = new Hashtable();
            filter["search-query"] = param;
            filter["branchID"] = param2;
            filter["receivingNoteDate"] = param3;
            filter["workOrderIDs"] = param4;
            filter["workCenterID"] = param5;

            Library.DTO.Notification notification = new Library.DTO.Notification();

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItem", filter, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-bom")]
        public IHttpActionResult GetBOM(string workOrderIDs, int branchID, string receivingNoteDate, int? workCenterID)
        {
            Hashtable filter = new Hashtable();
            filter["work-order-ids"] = workOrderIDs;
            filter["branchID"] = branchID;
            filter["receivingNoteDate"] = receivingNoteDate;
            filter["workCenterID"] = workCenterID;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetBOM", filter, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("GetListReceivingNote")]
        public IHttpActionResult ExportExcel(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetListReceivingNote", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.Reset(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("workOrder-Items")]
        public IHttpActionResult GetWorkOrderItems(int productionItemID)
        {
            Hashtable filters = new Hashtable();
            filters["productionItemID"] = productionItemID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkOrderItems", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("purchaseorderdetail")]
        public IHttpActionResult GetPurchaseOrderDetail(int purchaseOrderID, int branchID, string receivingNoteDate)
        {
            Hashtable filters = new Hashtable();
            filters["PurchaseOrderID"] = purchaseOrderID;
            filters["BranchID"] = branchID;
            filters["ReceivingNoteDate"] = receivingNoteDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPurchaseOrderDetail", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("setcontentdetail")]
        public IHttpActionResult SetContentDetail(int productionItemID, int factoryWarehouseID)
        {
            Hashtable filters = new Hashtable();
            filters["ProductionItemID"] = productionItemID;
            filters["FactoryWarehouseID"] = factoryWarehouseID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "SetContentDetail", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getBOMProductionItem")]
        public IHttpActionResult GetBOMProductionItem(string productionItemUD, string workOrderIDs, int? workCenterID, int? branchID, string receivingNoteDate)
        {
            Hashtable filters = new Hashtable();
            filters["ProductionItemUD"] = productionItemUD;
            filters["WorkOrderIDs"] = workOrderIDs;
            filters["WorkCenterID"] = workCenterID;
            filters["BranchID"] = branchID;
            filters["ReceivingNoteDate"] = receivingNoteDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getBOMProductionItem", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSearchDetail")]
        public IHttpActionResult GetDataForSearchDetail()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getSearchDetail", null, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("script-update-factoryproductionmonitiring")]
        public IHttpActionResult UpdateFactoryProductionMonitiring()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "script-update-factoryproductionmonitiring", null, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-factory-sale-order")]
        public IHttpActionResult GetFactorySaleOrder(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-factory-sale-order", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-factory-sale-order-detail")]
        public IHttpActionResult GetFactorySaleOrderDetail(int factorySaleOrderID, string receivingNoteDate)
        {
            Hashtable filters = new Hashtable();
            filters["factorySaleOrderID"] = factorySaleOrderID;
            filters["receivingNoteDate"] = receivingNoteDate;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-factory-sale-order-detail", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
