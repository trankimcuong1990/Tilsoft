using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/acc-manager-performance")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class AccManagerPerformanceRptController : ApiController
    {
		private string moduleCode = "AccManagerPerformanceRpt";
		private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.AccManagerPerformanceRpt.Executor, Module.AccManagerPerformanceRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification;
            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-acc-performance")]
        public IHttpActionResult GetAccPerformance(int saleId)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED } });
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["saleId"] = saleId;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-acc-performance", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-sales-by-country")]
        public IHttpActionResult GetSalesByCountry(int saleId)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Error, Message = Properties.Resources.NOT_AUTHORIZED } });
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["saleId"] = saleId;
            //object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-acc-performance", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success } });
        }
    }
}
