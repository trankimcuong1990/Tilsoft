using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/factory-breakdown")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class FactoryBreakdownMngController : ApiController
    {
        private string moduleCode = "FactoryBreakdownMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryBreakdownMng.Executor, Module.FactoryBreakdownMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
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
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
            executor.Reset(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("exportexcellistfactorybreakdown")]
        public IHttpActionResult ExportExcel(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcellistfactorybreakdown", searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdata")]
        public IHttpActionResult Get(int id, int? sampleProductID, int? modelID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return Ok(new Library.DTO.ReturnData<Object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = "Not authorized!" }, TotalRows = 0 });
            }
            Library.DTO.Notification notification;
            Hashtable input = new Hashtable();
            input["id"] = id;
            input["sampleProductID"] = sampleProductID;
            input["modelID"] = modelID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("export-excel-detail")]
        public IHttpActionResult ExportExcelDetail(int id)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportfactorybreakdowndetail", input, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-data-breakdown")]
        public IHttpActionResult GetDataForBreakdown(int sampleProductID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["sampleProductID"] = sampleProductID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdataforbreakdown", input, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getfilterdata")]
        public IHttpActionResult GetFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getfilterdata", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateImportData")]
        public IHttpActionResult ImportExcelData(object dtoItem)
        {
            Hashtable input = new Hashtable();
            input["dtoItem"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateimportdata", input, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
