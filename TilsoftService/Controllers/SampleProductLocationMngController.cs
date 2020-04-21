using DTO;
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
    [RoutePrefix("api/sampleProductLocation")]
    public class SampleProductLocationMngController : ApiController
    {
        private string moduleCode = "SampleProductLocationMng";
        private IExecutor executor = Library.Helper.GetDynamicObject("Module.SampleLocationMng.Executor, Module.SampleLocationMng");
        private Module.Framework.BLL bLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(Search searchInput)
        {
            // Declare.
            Notification notification;
            int totalRows;

            // Define.
            int userID = ControllerContext.GetAuthUserId();

            // Check.
            if (!bLL.CanPerformAction(userID, moduleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            object data = executor.GetDataWithFilter(userID, searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new ReturnData<object> { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            Notification notification;

            int userID = ControllerContext.GetAuthUserId();

            if (!bLL.CanPerformAction(userID, moduleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            object data = executor.GetData(userID, id, out notification);

            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            Notification notification;

            int userID = ControllerContext.GetAuthUserId();

            if (id > 0 && !bLL.CanPerformAction(userID, moduleCode, ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            executor.UpdateData(userID, id, ref dtoItem, out notification);

            return Ok(new ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getdata")]
        public IHttpActionResult GetDataWithIDs(DTO.Search searchInput)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", searchInput.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatedata")]
        public IHttpActionResult UpdateData(DTO.Search searchInput)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatedata", searchInput.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }
    }
}
