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
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/accountreceivablerpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class AccountReceivableRptController : ApiController
    {
        private string moduleCode_DetailOfLiabilities = "AccountReceivableRpt";
       
        private Library.Base.IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.AccountReceivableRpt.Executor, Module.AccountReceivableRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getliabilities")]
        public IHttpActionResult GetTotalLiabilities(System.Collections.Hashtable filters)
        {
            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_DetailOfLiabilities, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getliabilities", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("query-supplier")]
        public IHttpActionResult QuyerySupplier(string param)
        {
            Hashtable input = new Hashtable();
            input["query"] = param;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "query-supplier", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }
    }
}