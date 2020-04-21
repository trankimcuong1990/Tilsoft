using System;
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
    [RoutePrefix("api/receipt-Note")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class ReceiptNoteMngController : ApiController
    {
        private string moduleCode = "ReceiptNoteMng";
        private Library.Base.IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.ReceiptNoteMng.Executor, Module.ReceiptNoteMng");
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
        [Route("get-supplier")]
        public IHttpActionResult GetSupplier()
        {           
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-supplier", new Hashtable(), out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("gets-purchasing")]
        public IHttpActionResult GetsPurchasing(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-purchasing-invoice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, });
        }

        [HttpPost]
        [Route("set-status")]
        public IHttpActionResult SetStatus(int receiptNoteID, int statusID, int receiptNoteTypeID)
        {
            Hashtable input = new Hashtable();
            input["receiptNoteID"] = receiptNoteID;
            input["statusID"] = statusID;
            input["receiptNoteTypeID"] = receiptNoteTypeID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "set-status", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-receiptnote-printout")]
        public IHttpActionResult GetReceiptNotePrint(int receiptNoteID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["receiptNoteID"] = receiptNoteID;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-receiptnote-printout", input, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
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
        [Route("query-employee")]
        public IHttpActionResult QueryEmployee(string param)
        {
            Hashtable input = new Hashtable();
            input["query"] = param;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "query-employee", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("query-productionitem")]
        public IHttpActionResult QueryProductionItem(string param)
        {
            Hashtable input = new Hashtable();
            input["query"] = param;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "query-productionitem", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("gets-factory-sale-invoice")]
        public IHttpActionResult GetsFactorySaleInvoice(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-factory-sale-invoice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, });
        }
    }
}