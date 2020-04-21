using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.DTO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/costinvoice2typemng")]
    public class CostInvoice2TypeMngController : ApiController
    {
        const string ModuleCode = "CostInvoice2Type";
        readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.CostInvoice2TypeMng.Executor, Module.CostInvoice2TypeMng");
        readonly Module.Framework.BLL framework = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            input.Filters.Add("UserID", ControllerContext.GetAuthUserId());

            var data = this.executor.GetDataWithFilter(this.ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            var data = this.executor.GetData(this.ControllerContext.GetAuthUserId(), id, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object obj)
        {
            if (this.HasNotUpdatePermission(id, out IHttpActionResult httpActionResult))
                return httpActionResult;

            this.SetModuleIdenfitier(this.executor);

            this.executor.UpdateData(this.ControllerContext.GetAuthUserId(), id, ref obj, out Notification notification);
            return Ok(new ReturnData<object>() { Data = obj, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteData(int id)
        {
            if (!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanDelete))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            this.SetModuleIdenfitier(this.executor);

            this.executor.DeleteData(this.ControllerContext.GetAuthUserId(), id, out Notification notification);
            return Ok(new ReturnData<object>() { Data = id, Message = notification });
        }

        private void SetModuleIdenfitier(Library.Base.IExecutor executor)
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