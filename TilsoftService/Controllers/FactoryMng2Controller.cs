using Library.DTO;
using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/factorymng2")]
    public class FactoryMng2Controller : ApiController
    {
        private string moduleCode = "FactoryMng2";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryMng2.Executor, Module.FactoryMng2");
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

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            BLL.Framework fwBll = new BLL.Framework();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
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

        //
        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("detail")]
        public IHttpActionResult GetDetail(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdetail", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-factoryorder-turnover")]
        public IHttpActionResult GetFactoryOrderTurnover(int id, string season, string clientUD, string factoryOrderUD, string trackingStatusNM, int pageSize, int pageIndex, string orderBy, string orderDirection)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["FactoryID"] = id;
            filters["Season"] = season;
            filters["ClientUD"] = clientUD;
            filters["FactoryOrderUD"] = factoryOrderUD;
            filters["TrackingStatusNM"] = trackingStatusNM;
            filters["PageSize"] = pageSize;
            filters["PageIndex"] = pageIndex;
            filters["OrderBy"] = orderBy;
            filters["OrderDirection"] = orderDirection;
            filters["TotalRows"] = 0;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getfactoryorderturnover", filters, out Notification notification);

            int totalRows = filters.ContainsKey("TotalRows") && filters["TotalRows"] != null && !string.IsNullOrEmpty(filters["TotalRows"].ToString()) ? Convert.ToInt32(filters["TotalRows"].ToString()) : 0;

            return Ok(new ReturnData<object>() { Data = data, TotalRows = totalRows, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelfactory")]
        public IHttpActionResult ExportExcel(int type, DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            searchInput.Filters["Type"] = type;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcelfactory", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getpersonincharge")]
        public IHttpActionResult GetPersonInCharge(int supplierID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["supplierID"] = supplierID;
           
            filters["TotalRows"] = 0;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpersonincharge", filters, out Notification notification);

            int totalRows = filters.ContainsKey("TotalRows") && filters["TotalRows"] != null && !string.IsNullOrEmpty(filters["TotalRows"].ToString()) ? Convert.ToInt32(filters["TotalRows"].ToString()) : 0;

            return Ok(new ReturnData<object>() { Data = data, TotalRows = totalRows, Message = notification });
        }

    }
}
