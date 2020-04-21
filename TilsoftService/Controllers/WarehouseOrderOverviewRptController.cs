using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/warehouse-order-overview")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class WarehouseOrderOverviewRptController : ApiController
    {
        private string moduleCode = "WarehouseOrderOverviewRpt";
        private Library.Base.IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.WarehouseOrderOverviewRpt.Executor, Module.WarehouseOrderOverviewRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();
      
        [HttpPost]
        [Route("get-warehouse-sold-item")]
        public IHttpActionResult GetWarehouseSoldItem(string season)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable();
            param["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-warehouse-sold-item", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
