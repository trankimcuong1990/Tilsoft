using Library.Base;
using Library.DTO;
using System;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/opsequence")]
    public class OPSequenceController : ApiController
    {
        private const string ModuleCode = "OPSequence";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.OPSequence.Executor, Module.OPSequence");
        private readonly Module.Framework.BLL framework = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search input)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            var data = this.executor.GetDataWithFilter(this.ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
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
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanDelete))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            this.executor.DeleteData(this.ControllerContext.GetAuthUserId(), id, out Notification notification);
            return Ok(new ReturnData<object>() { Data = id, Message = notification });
        }

        private void SetModuleIdenfitier(IExecutor executor)
        {
            if (executor != null)
                executor.identifier = ControllerContext.GetCurrentUserFolder();
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id == 0 && !this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }
    }
}
