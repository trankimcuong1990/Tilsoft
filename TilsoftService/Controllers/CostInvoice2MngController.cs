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
    [RoutePrefix("api/costInvoice2Mng")]
    public class CostInvoice2MngController : ApiController
    {
        private const string ModuleCode = "CostInvoice2Mng";
        private readonly IExecutor executor = Library.Helper.GetDynamicObject("Module.CostInvoice2Mng.Executor, Module.CostInvoice2Mng");
        private readonly Module.Framework.BLL framework = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search input)
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
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
        public IHttpActionResult Update(int id, object obj)
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
        public IHttpActionResult Delete(int id)
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Notification notification);
            return Ok(new ReturnData<object>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object obj)
        {
            if (HasNotUpdatePermission(id, out IHttpActionResult httpActionResult))
            {
                return httpActionResult;
            }

            SetModuleIdenfitier(executor);

            executor.Approve(ControllerContext.GetAuthUserId(), id, ref obj, out Notification notification);
            return Ok(new ReturnData<object>() { Data = obj, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult Init()
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportcostinvoice2")]
        public IHttpActionResult ExportCostInvoice2(string season)
        {
            if (!framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["Season"] = season;
            //filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportcostinvoice2", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        private void SetModuleIdenfitier(IExecutor executor)
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

            if (id > 0 && !framework.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanApprove))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }
    }
}
