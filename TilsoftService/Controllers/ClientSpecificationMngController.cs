using DTO;
using Library.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Lib;
using TilsoftService.Helper;
using Library.DTO;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [LogFilterAtrribute]
    [RoutePrefix("api/clientSpecification")]
    public class ClientSpecificationMngController : ApiController
    {
        private const string moduleCode = "ClientSpecificationMng";

        private readonly IExecutor iExecutor = Library.Helper.GetDynamicObject("Module.ClientSpecificationMng.Executor, Module.ClientSpecificationMng");
        private readonly Module.Framework.BLL bll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(Search input)
        {
            int totalRows;
            Notification notification;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = iExecutor.GetDataWithFilter(ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out totalRows, out notification);

            return Ok(new ReturnData<object> { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            Notification notification;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = iExecutor.GetData(ControllerContext.GetAuthUserId(), id, out notification);

            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            Notification notification;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            iExecutor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);

            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("postComment")]
        public IHttpActionResult PostComment(object dtoItem)
        {
            Notification notification;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["data"] = dtoItem;

            object data = iExecutor.CustomFunction(ControllerContext.GetAuthUserId(), "PostComment", input, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deleteComment")]
        public IHttpActionResult DeleteComment(int id)
        {
            Notification notification;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["id"] = id;

            object data = iExecutor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteComment", input, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getStandardFile")]
        public IHttpActionResult GetStandardFile(int typeID, int clientID)
        {
            Notification notification;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["typeID"] = typeID;
            input["clientID"] = clientID;

            object data = iExecutor.CustomFunction(ControllerContext.GetAuthUserId(), "getstandardfile", input, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
