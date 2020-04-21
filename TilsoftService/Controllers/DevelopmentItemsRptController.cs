using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.DTO;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/developmentitemsrpt")]
    public class DevelopmentItemsRptController : ApiController
    {
        const string ModuleCode = "DevelopmentItemsRpt";
        readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.DevelopmentItemsRpt.Executor, Module.DevelopmentItemsRpt"); 
        readonly Module.Framework.BLL framework = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            //authencation
            if(!this.framework.CanPerformAction(this.ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            this.SetModuleIdenfitier(this.executor);
            var data = this.executor.GetDataWithFilter(this.ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
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