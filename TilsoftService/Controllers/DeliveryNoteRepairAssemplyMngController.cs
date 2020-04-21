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
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/DeliveryNoteRepairAssemplyMng")]
    public class DeliveryNoteRepairAssemplyMngController : ApiController
    {
        private string moduleCode = "DeliveryNoteRepairAssemplyMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.DeliveryNote2.Executor, Module.DeliveryNote2");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();


        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search input)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            input.Filters.Add("UserID", ControllerContext.GetAuthUserId());

            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
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
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            SetModuleIdenfitier(executor);

            var data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
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

            if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;
        }

        //[HttpPost]
        //[Route("gets")]
        //public IHttpActionResult Gets(DTO.Search searchInput)
        //{
        //    // authentication
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }
        //    Library.DTO.Notification notification;
        //    int totalRows = 0;
        //    Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
        //    return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        //}
    }
}
