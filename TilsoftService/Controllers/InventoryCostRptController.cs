using Library.DTO;
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
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/inventorycostrpt")]
    public class InventoryCostRptController : ApiController
    {
        private const string moduleCode = "InventoryCostRpt";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.InventoryCostRpt.Executor, Module.InventoryCostRpt");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            // Check permission end-user.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetDataWithFilters(int? factoryWarehouseID, int? productionItemTypeID, string startDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouseID"] = factoryWarehouseID;
            filters["productionItemTypeID"] = productionItemTypeID;
            filters["startDate"] = startDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithfilters", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(int? factoryWarehouseID, int? productionItemTypeID, string startDate)
        {
            // authentication
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouseID"] = factoryWarehouseID;
            filters["productionItemTypeID"] = productionItemTypeID;
            filters["startDate"] = startDate;

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
