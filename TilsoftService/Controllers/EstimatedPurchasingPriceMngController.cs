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
    [RoutePrefix("api/estimated-purchasing-price")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class EstimatedPurchasingPriceMngController : ApiController
    {
		private string moduleCode = "EstimatedPurchasingPriceMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.EstimatedPurchasingPriceMng.Executor, Module.EstimatedPurchasingPriceMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        //[HttpPost]
        //[Route("getsearchfilter")]
        //public IHttpActionResult GetSearchFilterData()
        //{
        //    object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out Library.DTO.Notification notification);
        //    return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        //}

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
            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }        

		[HttpPost]
        [Route("update")]
        public IHttpActionResult Update(object dtoItems)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["data"] = dtoItems;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-batch", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-factories")]
        public IHttpActionResult GetFactories()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-factories", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-history")]
        public IHttpActionResult GetHistory(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-history", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
