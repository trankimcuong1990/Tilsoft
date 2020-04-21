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
    [RoutePrefix("api/storageCardRpt")]
    public class StorageCardRptController : ApiController
    {
        private const string moduleCode = "StorageCardRpt";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.StorageCardRpt.Executor, Module.StorageCardRpt");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification;

            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(int productionItemID, int factoryWarehouseID, string startDate, string endDate)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable param = new System.Collections.Hashtable();

            param["productionItemID"] = productionItemID;
            param["factoryWarehouseID"] = factoryWarehouseID;
            param["startDate"] = startDate;
            param["endDate"] = endDate;

            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportStorageCard", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitfrominventoryoverview")]
        public IHttpActionResult GetInitFromInventoryOverview(int productionItemID, int factoryWarehouseID, string startDate, string endDate, int branchID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable param = new System.Collections.Hashtable();

            param["productionItemID"] = productionItemID;
            param["factoryWarehouseID"] = factoryWarehouseID;
            param["startDate"] = startDate;
            param["endDate"] = endDate;
            param["branchID"] = branchID;

            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinitfrominventoryoverview", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetDataWithFilters(int productionItemID, int factoryWarehouseID, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable param = new System.Collections.Hashtable();
            param["productionItemID"] = productionItemID;
            param["factoryWarehouseID"] = factoryWarehouseID;
            param["startDate"] = startDate;
            param["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithfilters", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }
    }
}
