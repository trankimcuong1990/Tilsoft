using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/payment-note")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class PaymentNoteController : ApiController
    {
        private string moduleCode = "PaymentNoteMng";
        private Library.Base.IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.PaymentNoteMng.Executor, Module.PaymentNoteMng");
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
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinitdata", null, out Library.DTO.Notification notification);
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
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, null, out Library.DTO.Notification notification);
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
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
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

        [HttpPost]
        [Route("query-supplier")]
        public IHttpActionResult QuyeryClient(string param)
        {
            Hashtable input = new Hashtable();
            input["query"] = param;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "query-supplier", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("gets-purchase-invoice")]
        public IHttpActionResult GetsEcommercial(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-purchase-invoice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, });
        }

        [HttpPost]
        [Route("set-status")]
        public IHttpActionResult SetStatus(int paymentNoteID, int statusID, int paymentNoteTypeID)
        {
            Hashtable input = new Hashtable();
            input["paymentNoteID"] = paymentNoteID;
            input["statusID"] = statusID;
            input["paymentNoteTypeID"] = paymentNoteTypeID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "set-status", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-paymentnote-print")]
        public IHttpActionResult GetPaymentNotePrint(int paymentNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["paymentNoteID"] = paymentNoteID;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-paymentnote-printout", input, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("get-paymentnote-paymentbydeposit")]
        public IHttpActionResult GetPaymentByDeposit(int supplierID, string year, string currency)
        {
            Hashtable input = new Hashtable();
            input["supplierID"] = supplierID;
            input["year"] = year;
            input["currency"] = currency;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-paymentbydeposit", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-paymentnote-totaldeposit")]
        public IHttpActionResult GetTotalDeposit(int supplierID, string year, string currency)
        {
            Hashtable input = new Hashtable();
            input["supplierID"] = supplierID;
            input["year"] = year;
            input["currency"] = currency;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-totaldeposit", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("search-po")]
        public IHttpActionResult SearchPO(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-po", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, });
        }

        [HttpPost]
        [Route("get-po-from-invoice")]
        public IHttpActionResult GetPOFromInvoice(int purchaseInvoiceID)
        {
            Hashtable input = new Hashtable();
            input["purchaseInvoiceID"] = purchaseInvoiceID;

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-po-from-invoice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, });
        }
    }
}