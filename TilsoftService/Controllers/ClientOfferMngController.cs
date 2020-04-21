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
    [RoutePrefix("api/ClientOfferMng")]
    public class ClientOfferMngController : ApiController
    {
        private string moduleCode = "ClientOfferMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ClientOfferMng.Executor, Module.ClientOfferMng");
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
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
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
        [Route("getClientCostItem")]
        public IHttpActionResult GetClientCostItem(int clientCostItemID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["clientCostItemID"] = clientCostItemID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetClientCostItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getClientConditionItem")]
        public IHttpActionResult GetClientConditionItem(int clientConditionItemID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["clientConditionItemID"] = clientConditionItemID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetClientConditionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateClientCostItem")]
        public IHttpActionResult UpdateClientCostItem(int clientCostItemID, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["clientCostItemID"] = clientCostItemID;
            filters["dtoItem"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateClientCostItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateClientConditionItem")]
        public IHttpActionResult UpdateClientConditionItem(int clientConditionItemID, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["clientConditionItemID"] = clientConditionItemID;
            filters["dtoItem"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateClientConditionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getReportClientOfferOverview")]
        public IHttpActionResult GetReportClientOfferOverview(string validFrom, string validTo)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["validFrom"] = validFrom;
            filters["validTo"] = validTo;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetReportClientOfferOverview", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteClientCostItem")]
        public IHttpActionResult DeleteClientCostItem(int clientCostItemID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["clientCostItemID"] = clientCostItemID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteClientCostItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteClientConditionItem")]
        public IHttpActionResult DeleteClientConditionItem(int clientConditionItemID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["clientConditionItemID"] = clientConditionItemID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteClientConditionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getClientOfferPrintout")]
        public IHttpActionResult GetClientOfferPrintout(int clientOfferID, int printOptionValue)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();

            filters["clientOfferID"] = clientOfferID;
            filters["printOptionValue"] = printOptionValue;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ClientOffer_Printout", filters, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
