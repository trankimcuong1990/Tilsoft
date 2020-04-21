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
    [RoutePrefix("api/scmagendamng")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class SCMAgendaMngController : ApiController
    {
		private string moduleCode = "SCMAgendaMng";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.SCMAgendaMng.Executor, Module.SCMAgendaMng");
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

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
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
            object data = executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
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
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getsupportdata")]
        public IHttpActionResult GetSupportData(string formname)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["formName"] = formname;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsupportdata", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpGet]
        [Route("quicksearchclient")]
        public IHttpActionResult QuickSearchClient(string query)
        {
            Library.DTO.Notification notification;
            Hashtable input = new Hashtable();
            input["query"] = query;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "searchclient", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("listqcreport")]
        public IHttpActionResult ListQCReport(string qcReportUD, int? factoryID, string clientUD, string articleCode, string proformaInvoiceNo)
        {
            Library.DTO.Notification notification;
            Hashtable input = new Hashtable();
            input["qcReportUD"] = qcReportUD;
            input["factoryID"] = factoryID;
            input["clientUD"] = clientUD;
            input["articleCode"] = articleCode;
            input["proformaInvoiceNo"] = proformaInvoiceNo;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "listqcreport", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
