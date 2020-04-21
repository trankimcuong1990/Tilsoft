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
    [RoutePrefix("api/factoryLocationMng")]
    public class FactoryLocationMngController : ApiController
    {
        private const string moduleCode = "FactoryLocationMng";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryLocationMng.Executor, Module.FactoryLocationMng");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(DTO.Search searchInput)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, TotalRows = totalRows, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            if (id == 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id > 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteData(int id)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = id, Message = notification });
        }
    }
}
