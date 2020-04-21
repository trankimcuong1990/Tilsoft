using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.DTO;
using TilsoftService.Helper;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/wexstockoverviewrpt")]
    public class WEXStockOverviewRptController : ApiController
    {
        private string moduleCode = "WEXStockOverviewRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.WEXStockOverviewRpt.Executor, Module.WEXStockOverviewRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("exportexcel")]
        public IHttpActionResult ExportExcel(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = searchInput.Filters;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcel", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("updatesellingprice")] 
        public IHttpActionResult UpdateSellingPrice(object priceData)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            Hashtable input = new Hashtable();
            input["data"] = priceData;
            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatesellingprice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatepurchasingprice")]

        public IHttpActionResult UpdatePurchasingPrice(object pPurchasingData)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();

            Hashtable input = new Hashtable();
            input["data"] = pPurchasingData;

            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatepurchasingprice", input, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateobsolete")]

        public IHttpActionResult UpdateObsolete(object dataOject)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();

            Hashtable input = new Hashtable();
            input["data"] = dataOject;

            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateobsolete", input, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatevalueobsolescence")]

        public IHttpActionResult UpdateValueObsolescence(object dataOject)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();

            Hashtable input = new Hashtable();
            input["data"] = dataOject;

            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatevalueobsolescence", input, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updateProductPrice")]
        public IHttpActionResult UpdateProductPrice(object param)
        {
            //authentication
            if (!fwBll.CanPerformAction(this.ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            Hashtable filters = new Hashtable();
            filters["data"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateproductprice", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update-enable-status")]
        public IHttpActionResult UpdateEnableStatus(object data)
        {
            //authentication
            if (!fwBll.CanPerformAction(this.ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            Hashtable param = new Hashtable();
            param["data"] = data;

            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-enable-status", param, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

    }
}
