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
    [RoutePrefix("api/preorderplanningoverviewrpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class PreOrderPlanningOverviewRptController : ApiController
    {
		private string moduleCode = "PreOrderPlanningOverviewRpt";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PreOrderPlanningOverviewRpt.Executor, Module.PreOrderPlanningOverviewRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();
        
        [HttpPost]
        [Route("gethtmldata")]
        public IHttpActionResult GetInitData()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "gethtmldata", null, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
