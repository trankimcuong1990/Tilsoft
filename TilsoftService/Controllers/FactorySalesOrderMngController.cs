using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/FactorySalesOrderMng")]
    public class FactorySalesOrderMngController : ApiController
    {
        // GET: FactorySalesOrderMng        
        private string moduleCode = "FactorySalesOrderMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactorySalesOrderMng.Executor, Module.FactorySalesOrderMng");
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
        public IHttpActionResult Get(int id, Hashtable param)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            param["id"] = id;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("getFilterOrder")]
        public IHttpActionResult GetFilterQuotation(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getFilterOrder", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFilterQuotation")]
        public IHttpActionResult GetFilterOrder(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFilterQuotation", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("getFilterRawMaterial")]
        public IHttpActionResult GetFilterRawMaterial(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFilterRawMaterial", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("getFilterSaleEmployee")]
        public IHttpActionResult GetFilterSaleEmployee(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFilterSaleEmployee", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }
        
        [HttpPost]
        [Route("getFilterProductItem")]
        public IHttpActionResult GetFilterProductItem(string param)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFilterProductItem", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("getFilterClientcontact")]
        public IHttpActionResult GetFilterClientcontact(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetFilterClientcontact", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
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
        [Route("getfactorysalesquotation")]
        public IHttpActionResult GetFactorySalesQuotation(string param)
        {
            Hashtable filters = new Hashtable();
            filters["factorySalesQuotationUD"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getfactorysalesquotation", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Notification notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
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
    }
}