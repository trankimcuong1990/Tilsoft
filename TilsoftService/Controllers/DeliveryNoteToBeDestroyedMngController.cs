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
    [RoutePrefix("api/deliveryNoteToBeDestroyedMng")]
    public class DeliveryNoteToBeDestroyedMngController : ApiController
    {
        private const string ModuleCode = "ProductionTeam";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.DeliveryNote2.Executor, Module.DeliveryNote2");
        private readonly Module.Framework.BLL framework = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            input.Filters.Add("UserID", ControllerContext.GetAuthUserId());

            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            var data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object obj)
        {
            if (HasNotUpdatePermission(id, out IHttpActionResult httpActionResult))
            {
                return httpActionResult;
            }

            SetModuleIdenfitier(executor);

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref obj, out Notification notification);

            return Ok(new ReturnData<object>() { Data = obj, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteData(int id)
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Notification notification);

            return Ok(new ReturnData<object>() { Data = id, Message = notification });
        }

        private void SetModuleIdenfitier(Library.Base.IExecutor executor)
        {
            if (executor != null)
            {
                executor.identifier = ControllerContext.GetCurrentUserFolder();
            }
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id == 0 && !framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }
    }
}
