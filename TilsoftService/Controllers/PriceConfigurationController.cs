using Library.DTO;
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
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/priceconfiguration")]
    public class PriceConfigurationController : ApiController
    {
        private const string moduleCode = "PriceConfiguration";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PriceConfiguration.Executor, Module.PriceConfiguration");
        private readonly Module.Framework.BLL bll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(DTO.Search searchInput)
        {
            Notification notification = null;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            SetModuleIdentifier(executor);

            int totalRows = 0;
            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id, string season)
        {
            Notification notification = null;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            SetModuleIdentifier(executor);

            Hashtable input = new Hashtable()
            {
                ["Id"] = id,
                ["Season"] = (string.IsNullOrEmpty(season) ? string.Empty : season)
            };

            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdatawithseason", input, out notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            Notification notification = null;

            IHttpActionResult httpActionResult;
            if (HasNotUpdatePermission(id, out httpActionResult))
                return httpActionResult;

            SetModuleIdentifier(executor);

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);

            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteData(int id, string season)
        {
            Notification notification = null;

            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanDelete))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            SetModuleIdentifier(executor);

            Hashtable input = new Hashtable() { ["Season"] = season };

            //executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletewithseason", input, out notification);

            return Ok(new ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("allsupportwithseason")]
        public IHttpActionResult GetAllSupportWithSeason(DTO.Search searchInput)
        {
            //Notification notification = null;

            //if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            //SetModuleIdentifier(executor);

            //int totalRows = 0;
            //var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(/*new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows }*/);
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id > 0 && !bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id == 0 && !bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }

        private void SetModuleIdentifier(Library.Base.IExecutor myExecutor)
        {
            if (myExecutor != null)
                myExecutor.identifier = ControllerContext.GetCurrentUserFolder();
        }
    }
}
