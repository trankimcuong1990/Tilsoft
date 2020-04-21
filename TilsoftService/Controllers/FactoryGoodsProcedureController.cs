using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/factoryGoodsProcedure")]
    public class FactoryGoodsProcedureController : ApiController
    {
        private string moduleCode = "FactoryGoodsProcedureMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryGoodsProcedure.Executor, Module.FactoryGoodsProcedure");
        Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // Authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
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
            // Authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            bool isCanUpdate = fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate);
            bool isCanCreate = fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate);

            // Check authentication.
            if ((id > 0 && !isCanUpdate) || (id == 0 && !isCanCreate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            Library.DTO.Notification notification;
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            bool isCanDelete = fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete);

            // Check authentication.
            if (!isCanDelete)
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);

            return Ok(new Library.DTO.ReturnData<int> { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("createDetail")]
        public IHttpActionResult CreateDetail(int factoryGoodsProcedureID, object factoryGoodsProcedureDetail)
        {
            Library.DTO.Notification notification;
            Hashtable filter = new Hashtable();
            filter["factoryGoodsProcedureID"] = factoryGoodsProcedureID;
            filter["factoryGoodsProcedureDetail"] = factoryGoodsProcedureDetail;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "EditDetail", filter, out notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = factoryGoodsProcedureID, Message = notification });
        }

        [HttpPost]
        [Route("deleteDetail")]
        public IHttpActionResult DeleteDetail(int factoryGoodsProcedureDetailID)
        {
            Library.DTO.Notification notification;
            Hashtable filter = new Hashtable();
            filter["factoryGoodsProcedureDetailID"] = factoryGoodsProcedureDetailID;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteDetail", filter, out notification);
            return Ok(new Library.DTO.ReturnData<int> { Data = factoryGoodsProcedureDetailID, Message = notification });
        }
    }
}
