namespace TilsoftService.Controllers
{
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
    using TilsoftService.Lib;

    [Authorize]
    [LogFilterAtrribute]
    [RoutePrefix("api/deliverynote")]
    public class DeliveryNoteController : ApiController
    {
        private readonly string moduleCode = "DeliveryNote";
        private readonly IExecutor executor = Library.Helper.GetDynamicObject("Module.DeliveryNote.Executor, Module.DeliveryNote");
        private readonly Module.Framework.BLL framework = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(Search input)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), this.moduleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            var data = this.executor.GetDataWithFilter(this.ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), this.moduleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            var data = this.executor.GetData(this.ControllerContext.GetAuthUserId(), id, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object obj)
        {
            if (this.HasNotUpdatePermission(id, out IHttpActionResult httpActionResult))
                return httpActionResult;

            this.SetModuleIdenfitier(this.executor);

            this.executor.UpdateData(this.ControllerContext.GetAuthUserId(), id, ref obj, out Notification notification);
            return Ok(new ReturnData<object>() { Data = obj, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), this.moduleCode, ModuleAction.CanDelete))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            this.executor.DeleteData(this.ControllerContext.GetAuthUserId(), id, out Notification notification);
            return Ok(new ReturnData<object>() { Data = id, Message = notification });
        }

        private void SetModuleIdenfitier(IExecutor executor)
        {
            if (executor != null)
                executor.identifier = this.ControllerContext.GetCurrentUserFolder();
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id == 0 && !this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), this.moduleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), this.moduleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }
    }
}
