using Library.Base;
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
    [RoutePrefix("api/inventoryRpt")]
    public class InventoryRptController : ApiController
    {
        private const string moduleCode = "InventoryRpt";
        private readonly IExecutor executor = Library.Helper.GetDynamicObject("Module.InventoryRpt.Executor, Module.InventoryRpt");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        /// <summary>
        /// API init
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// API export excel
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("exportinventoryreport")]
        public IHttpActionResult ExportInventoryReport(int factoryWarehouseID, int productionTeamID, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouseID"] = factoryWarehouseID;
            filters["productionTeamID"] = productionTeamID;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportinventoryreport", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetDataWithFilters(int factoryWarehouseID, int productionTeamID, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouseID"] = factoryWarehouseID;
            filters["productionTeamID"] = productionTeamID;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithfilters", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportinventoryreportdetail")]
        public IHttpActionResult ExportInventoryReportDetail(int factoryWarehouseID, int productionTeamID, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouseID"] = factoryWarehouseID;
            filters["productionTeamID"] = productionTeamID;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportinventoryreportdetail", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get-init-custom")]
        public IHttpActionResult GetInitCustom(int? branchID)
        {
            // Check permission end-user.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["BranchID"] = branchID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetInitCustom", filters, out Notification notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-data-filter-custom")]
        public IHttpActionResult GetDataFilterCustom(string factoryWarehouses, int? productionTeamID, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouses"] = factoryWarehouses;
            filters["productionTeamID"] = productionTeamID;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "customgetdatafilter", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
        
        [HttpPost]
        [Route("export-data-filter-custom")]
        public IHttpActionResult ExportDataFilterCustom(string factoryWarehouses, int? productionTeamID, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["factoryWarehouses"] = factoryWarehouses;
            filters["productionTeamID"] = productionTeamID;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "customexportdatafilter", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
