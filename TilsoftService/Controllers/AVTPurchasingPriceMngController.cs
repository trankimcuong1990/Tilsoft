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
    [RoutePrefix("api/avt-purchasing-price-mng")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class AVTPurchasingPriceMngController : ApiController
    {
		private string moduleCode = "AVTPurchasingPriceMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.AVTPurchasingPriceMng.Executor, Module.AVTPurchasingPriceMng");
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
        [Route("update-target")]
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
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatetarget", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("confirm-price")]
        public IHttpActionResult ConfirmPrice(object dtoItem)
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
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "confirmprice", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("un-confirm-price")]
        public IHttpActionResult UnConfirmPrice(object dtoItem)
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
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "unconfirmprice", param, out notification);
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
        [Route("get-purchasing-history")]
        public IHttpActionResult GetPurchasingHistory(int id)
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
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpurchasinghistory", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-general-breakdown")]
        public IHttpActionResult GetGeneralBreakDown(int id)
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
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getgeneralbreakdown", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-pal-breakdown")]
        public IHttpActionResult GetPALBreakDown(int id)
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
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpalbreakdown", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("export-excel")]
        public IHttpActionResult GetExportToExcel(DTO.Search input)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "export-to-excel", input.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-search-result-additional-data")]
        public IHttpActionResult GetSearchResultAdditionalData(string season, string ids)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable();
            param["season"] = season;
            param["ids"] = ids;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-search-result-additional-data", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

    }
}
