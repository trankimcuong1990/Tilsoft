using System;
using System.Web.Http;
using Library.DTO;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/transportcostforwarder")]
    public class TransportCostForwarderController : ApiController
    {
        private const string ModuleCode = "TransportCostForwarder";
        private readonly Library.Base.IExecutor _executor = Library.Helper.GetDynamicObject("Module.TransportCostForwarder.Executor, Module.TransportCostForwarder");
        private readonly Module.Framework.BLL _fwBll = new Module.Framework.BLL();


        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Notification notification;

            if (!_fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdentifier(_executor);

            int totalRows;
            var data = _executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            Notification notification;

            if (!_fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdentifier(_executor);

            var data = _executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            Notification notification;

            IHttpActionResult httpActionResult;
            if (HasNotUpdatePermission(id, out httpActionResult)) return httpActionResult;

            SetModuleIdentifier(_executor);

            _executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });

        }


        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Notification notification;

            if (!_fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdentifier(_executor);

            _executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchforwarder")]
        public IHttpActionResult QuickSearchForwarders(DTO.Search searchInput)
        {
            //get input
            var filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Notification notification;
            var data = _executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchforwarder", filters, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchbooking")]
        public IHttpActionResult QuickSearchBookings(DTO.Search searchInput)
        {
            //get input
            var filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Notification notification;
            var data = _executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchbooking", filters, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdropdowncontainer")]
        public IHttpActionResult GetDropDownContainer(DTO.Search searchInput)
        {
            var filters = searchInput.Filters;

            Notification notification;

            var data = _executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdropdowncontainer", filters, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getpriceperunit")]
        public IHttpActionResult GetPricePerUnit(DTO.Search searchInput)
        {
            // Declare Notification, filters, data;
            Notification notification;
            Hashtable filters;
            object data;

            // Set value filters, data
            filters = searchInput.Filters;
            data = _executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpriceperunit", filters, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id > 0 && !_fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode,
                    ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id == 0 && !_fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode,
                    ModuleAction.CanCreate))
            {

                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;

            }
            return false;
        }

        private void SetModuleIdentifier(Library.Base.IExecutor myExecutor)
        {
            if (myExecutor != null)
                myExecutor.identifier = ControllerContext.GetCurrentUserFolder();
        }
    }
}
