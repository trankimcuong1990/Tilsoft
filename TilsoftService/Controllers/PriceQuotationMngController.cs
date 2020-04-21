using DTO;
using Library.Base;
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
    [RoutePrefix("api/priceQuotation")]
    public class PriceQuotationMngController : ApiController
    {
        private string moduleCode = "PriceQuotationMng";
        private IExecutor executor = Library.Helper.GetDynamicObject("Module.PriceQuotationMng.Executor, Module.PriceQuotationMng");
        private Module.Framework.BLL bLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(Search inputSearch)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), inputSearch.Filters, inputSearch.PageSize, inputSearch.PageIndex, inputSearch.SortedBy, inputSearch.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult GetData(int id, object dtoItem)
        {
            if (id == 0 && !bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Notification notification);
            return Ok(new ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getprice")]
        public IHttpActionResult GetPriceSeason(Search inputSearch)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPrice", inputSearch.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateDifferenceCode")]
        public IHttpActionResult UpdateAllDifferenceCode(int id, Search inputSearch)
        {
            if (id != 0 && !bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateAllDifferenceCode", inputSearch.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Message = notification });
        }

        [HttpPost]
        [Route("getDataForPopupWithFilters")]
        public IHttpActionResult GetDataForPopupWithFilters(DTO.Search filters)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetDataForPopupWithFilters", filters.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPricePreviousSeason")]
        public IHttpActionResult GetPricePreviousSeason(DTO.Search filters)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPricePreviousSeason", filters.Filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult UnconfirmData(int id, object dtoItem)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("initdata")]
        public IHttpActionResult GetInitData(DTO.Search searchInput)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetInitData", searchInput.Filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update2")]
        public IHttpActionResult UpdateData(object dtoItems)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable dataItems = new Hashtable() { ["data"] = dtoItems };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateData", dataItems, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }
    }
}
