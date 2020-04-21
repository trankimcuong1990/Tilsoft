using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/factoryplc")]
    public class FactoryPLCMngController : ApiController
    {
        private string moduleCode = "FactoryPLCMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryPLCMng.Executor, Module.FactoryPLCMng");
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
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinitdata", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpGet]
        [Route("quicksearchbooking")]
        public IHttpActionResult QuickSearchBooking(int? factoryid, string query)
        {
            if (!factoryid.HasValue)
            {
                factoryid = -1;
            }

            Library.DTO.Notification notification;
            Hashtable input = new Hashtable();
            input["query"] = query;
            input["factoryid"] = factoryid;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "searchbooking", input, out notification);
            return Ok(data);
        }

        [HttpGet]
        [Route("quicksearchitem")]
        public IHttpActionResult QuickSearchItem(int? factoryid, int? bookingid, string query)
        {
            if (!factoryid.HasValue)
            {
                factoryid = -1;
            }
            if (!bookingid.HasValue)
            {
                bookingid = -1;
            }

            Library.DTO.Notification notification;
            Hashtable input = new Hashtable();
            input["query"] = query;
            input["factoryid"] = factoryid;
            input["bookingid"] = bookingid;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "searchitem", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int b, int f, int o, int ofs, int os)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            input["bookingid"] = b;
            input["factoryid"] = f;
            input["offerlineid"] = o;
            input["offerlinesampleproductid"] = ofs;
            input["offerlinesparepartid"] = os;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
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

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(string plcids)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["ids"] = plcids;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreport", input, out notification).ToString();
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return Ok(new Library.DTO.ReturnData<string>() { Data = notification.Message, Message = notification });
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
