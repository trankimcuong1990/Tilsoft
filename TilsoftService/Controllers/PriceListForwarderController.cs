namespace TilsoftService.Controllers
{
    using Library.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Helper;

    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/pricelistforwarder")]
    public class PriceListForwarderController : ApiController
    {
        private string moduleCode = "PriceListForwarder";
        private IExecutor executor = Library.Helper.GetDynamicObject("Module.PriceListForwarder.Executor, Module.PriceListForwarder");
        private Module.Framework.BLL bll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search search)
        {
            // Check authentication
            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // Get list data
            Library.DTO.Notification notification;
            int totalRows;

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), search.Filters, search.PageSize, search.PageIndex, search.SortedBy, search.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            // Check authentication
            if (!bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // Get data
            Library.DTO.Notification notification;

            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            // Check authentication
            if ((id > 0 && !bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate)) || (id == 0 && !bll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate)))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = dtoItem, Message = notification });
        }
    }
}
