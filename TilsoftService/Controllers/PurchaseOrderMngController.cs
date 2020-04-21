using Library.DTO;
using System;
using System.Collections;
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
    [RoutePrefix("api/PurchaseOrderMng")]
    public class PurchaseOrderMngController : ApiController
    {
        private string moduleCode = "PurchaseOrderMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PurchaseOrderMng.Executor, Module.PurchaseOrderMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(DTO.Search searchInput)
        {
            //authentication
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
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            var data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
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
        [Route("getPurchaseRequest")]
        public IHttpActionResult GetPurchaseRequest(string param, int param2)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;
            filters["searchQuery2"] = param2;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getPurchaseRequest", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPurchaseOrderDetailByPurchaseRequestID")]
        public IHttpActionResult GetPurchaseOrderDetailByPurchaseRequestID(int purchaseRequestID, int factoryRawMaterialID, string purchaseOrderDate)
        {
            Hashtable filters = new Hashtable();
            filters["PurchaseRequestID"] = purchaseRequestID;
            filters["FactoryRawMaterialID"] = factoryRawMaterialID;
            filters["purchaseOrderDate"] = purchaseOrderDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getPurchaseOrderDetailByPurchaseRequestID", filters, out Notification notification);

            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Notification notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }     

        [HttpPost]
        [Route("get-purchase-quotation-detail")]
        public IHttpActionResult GetPurchaseQuotationDetail(int factoryRawMaterialID, string purchaseOrderDate)
        {
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["factoryRawMaterialID"] = factoryRawMaterialID;
            inputs["purchaseOrderDate"] = purchaseOrderDate;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPurchaseQuotationDetail", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gethtmlreport")]
        public IHttpActionResult GetReceivingNotePrintoutHTML(int purchaseOrderID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["purchaseOrderID"] = purchaseOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetHTMLReportData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPurchaseOrderPrintout")]
        public IHttpActionResult GetDeliveryNotePrintout(int purchaseOrderID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["purchaseOrderID"] = purchaseOrderID;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPurchaseOrderPrintout", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getSupplierPaymentTerm")]
        public IHttpActionResult GetSupplierPaymentTerm(int factoryRawMaterialID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryRawMaterialID"] = factoryRawMaterialID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetSupplierPaymentTerm", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("cancel")]
        public IHttpActionResult Cancel(int id, object dtoItem)
        {
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["dtoItem"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "cancel", filters, out Notification notification);

            return Ok(new ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("finish")]
        public IHttpActionResult Finish(int id, object dtoItem)
        {
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["dtoItem"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "finish", filters, out Notification notification);

            return Ok(new ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("revise")]
        public IHttpActionResult Revise(int id, object dtoItem)
        {
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["dtoItem"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "revise", filters, out Notification notification);

            return Ok(new ReturnData<object> { Data = dtoItem, Message = notification });
        }
    }
}
