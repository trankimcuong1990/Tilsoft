using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using Library.DTO;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/clientcomplaints")]
    public class ClientComplaintController : ApiController
    {
        private readonly string moduleCode = "ClientComplaint";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ClientComplaint.Executor, Module.ClientComplaint");
        private readonly Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Notification notification;
            var data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int clientId, string season)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            input["clientId"] = clientId;
            input["season"] = season ?? string.Empty;

            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification;
            var totalRows = 0;
            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchfactoryorderdetails")]
        public IHttpActionResult QuickSearchFactoryOrderDetails(DTO.Search searchInput)
        {
            //get input
            var filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Notification notification;
            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchfactoryorderdetails", filters, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchPIs")]
        public IHttpActionResult QuicksearchPIs(DTO.Search searchInput)
        {
            //get input
            var filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Notification notification;
            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchPIs", filters, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification;
            //set indenfier is tempFolder which to use to save image
            executor.identifier = ControllerContext.GetCurrentUserFolder();
            //update data
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            //Library.DTO.Notification notification;
            //var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            var notification = new Notification();
            var data = new List<Object>();
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Notification notification;
            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Notification notification;
            executor.Reset(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }


        [HttpPost]
        [Route("getshipmentstatusofpisolution")]
        public IHttpActionResult GetShipmentStatusOfPiSolution(DTO.Search searchInput)
        {
            //get input
            var filters = searchInput.Filters;
            filters["pageSize"] = searchInput.PageSize;
            filters["pageIndex"] = searchInput.PageIndex;
            filters["sortedBy"] = searchInput.SortedBy;
            filters["sortedDirection"] = searchInput.SortedDirection;

            //get data
            Notification notification;
            executor.identifier = ControllerContext.GetCurrentUserFolder();
            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getshipmentstatusofpisolution", filters, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelclientcomplaint")]
        public IHttpActionResult ExportExcelClientComplaint()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification;
            executor.identifier = ControllerContext.GetCurrentUserFolder();
            var dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcelclientcomplaint", new Hashtable(), out notification);
            return Ok(new ReturnData<string>() { Data = dataFileName.ToString(), Message = notification });
        }

        [HttpPost]
        [Route("exportexcelclientcomplaintitem")]
        public IHttpActionResult ExportExcelClientComplaintItem(DTO.Search searchInput)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

         
            Notification notification;
            executor.identifier = ControllerContext.GetCurrentUserFolder();
            var dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcelclientcomplaintitem", searchInput.Filters, out notification);
            return Ok(new ReturnData<string>() { Data = dataFileName.ToString(), Message = notification });
        }
    }
}
