namespace TilsoftService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using TilsoftService.Helper;

    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/pricelistclientcharge")]
    public class PriceListClientChargeController : ApiController
    {
        #region ** Variable & Constant **

        string moduleCode = "PriceListClientCharge";

        Library.Base.IExecutor iExecutor = Library.Helper.GetDynamicObject("Module.PriceListClientCharge.Executor, Module.PriceListClientCharge");

        Module.Framework.BLL frameworkBll = new Module.Framework.BLL();

        #endregion

        #region ** Constructors **

        #endregion

        #region ** Function & Method **

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(DTO.Search searchInput)
        {
            int totalRows = 0;
            Library.DTO.Notification notification = null;

            // Check authentication
            if (!frameworkBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            object data = iExecutor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            Library.DTO.Notification notification = null;

            // Check authentication
            if (!frameworkBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            object data = iExecutor.GetData(ControllerContext.GetAuthUserId(), id, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            Library.DTO.Notification notification = null;

            // Check authentication
            if ((id > 0 && !frameworkBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate)) || (id == 0 && !frameworkBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate)))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            iExecutor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        #endregion
    }
}
