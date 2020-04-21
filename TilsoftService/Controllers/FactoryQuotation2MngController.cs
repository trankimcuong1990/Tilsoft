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
    [RoutePrefix("api/factory-quotation-2")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class FactoryQuotation2MngController : ApiController
    {
		private string moduleCode = "FactoryQuotation2Mng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryQuotation2Mng.Executor, Module.FactoryQuotation2Mng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

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
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update-price")]
        public IHttpActionResult UpdateTarget(object dtoItem)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-price", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-history")]
        public IHttpActionResult GetHistory(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "gethistory", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("export")]
        public IHttpActionResult Export(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param = searchInput.Filters;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "export", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
        [HttpPost]
        [Route("import")]
        public IHttpActionResult Import(object dtoItem)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["data"] = dtoItem;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "import", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-additionalcondition")]
        public IHttpActionResult GetACData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-additionalcondition", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
