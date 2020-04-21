using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/DeliveryNote2")]
    public class DeliveryNote2Controller : ApiController
    {
        private string moduleCode = "DeliveryNote2Mng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.DeliveryNote2.Executor, Module.DeliveryNote2");
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
        public IHttpActionResult Get(int id, string workOrderIDs, int branchID)
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
        [Route("getDeliveryNotePrintout")]
        public IHttpActionResult GetDeliveryNotePrintout(int deliveryNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["deliveryNoteID"] = deliveryNoteID;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetDeliveryNotePrintout", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getDeliveryNotePrintoutHTML")]
        public IHttpActionResult GetDeliveryNotePrintoutHTML(int deliveryNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["deliveryNoteID"] = deliveryNoteID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetDeliveryNotePrintoutHTML", filters, out notification);
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
        [Route("getOverView")]
        public IHttpActionResult GetOverView(int id)
        {
            //authencation
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable
            {
                ["id"] = id
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetOverView", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItem")]
        public IHttpActionResult GetProductionItem(string param, string param2)
        {
            //get data
            Hashtable filter = new Hashtable();
            filter["search-query"] = param;
            filter["deliveryNoteDate"] = param2;
            filter["branchID"] = ControllerContext.GetAuthUserBranchID();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItem", filter, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getBOM")]
        public IHttpActionResult GetBOM(string workOrderIDs, int branchID, string deliveryNoteDate)
        {
            Hashtable filter = new Hashtable();
            filter["workOrderIDs"] = workOrderIDs;
            filter["branchID"] = branchID;
            filter["DeliveryNoteDate"] = deliveryNoteDate;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetBOM", filter, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFactorySaleOrder")]
        public IHttpActionResult GetFactorySaleOrder(string param)
        {
            Hashtable filter = new Hashtable();
            filter["factorySaleOrderUD"] = param;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFactorySaleOrder", filter, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("GetListDeliveryNote")]
        public IHttpActionResult ExportExcel(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetListDeliveryNote", searchInput.Filters, out notification);
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
        [Route("purchaseorderquicksearch")]
        public IHttpActionResult PurchaseOrderQuicksearch(string param, string param2)
        {
            Hashtable filters = new Hashtable();
            filters["PurchaseOrderUD"] = param;
            filters["BranchID"] = param2;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "purchaseorderquicksearch", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("purchaseorderdetailquicksearch")]
        public IHttpActionResult PurchaseOrderDetailQuicksearch(int purchaseOrderID, int branchID, string deliveryNoteDate)
        {
            Hashtable filters = new Hashtable();
            filters["PurchaseOrderID"] = purchaseOrderID;
            filters["BranchID"] = branchID;
            filters["DeliveryNoteDate"] = deliveryNoteDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "purchaseorderdetailquicksearch", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("factorysaleorderdetailquicksearch")]
        public IHttpActionResult FactorySaleOrderDetailQuicksearch(int factorySaleOrderID, int branchID, string deliveryNoteDate)
        {
            Hashtable filters = new Hashtable();
            filters["FactorySaleOrderID"] = factorySaleOrderID;
            filters["BranchID"] = branchID;
            filters["DeliveryNoteDate"] = deliveryNoteDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "factorysaleorderdetailquicksearch", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
