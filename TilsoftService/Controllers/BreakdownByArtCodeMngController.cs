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
    [RoutePrefix("api/tilsoftservice/breakdown-by-art-code")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class BreakdownByArtCodeMngController : ApiController
    {
		private string moduleCode = "BreakdownByArtCodeMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BreakdownByArtCodeMng.Executor, Module.BreakdownByArtCodeMng");
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
    }
}
