using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;
using Library.DTO;

namespace TilsoftService.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/accountpayablerpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class AccountPayableRptController : ApiController
    {
        private string moduleCode_DetailOfLiabilities = "AccountPayableRpt";
        //private string moduleCode_CTPThu = "LiabilitiesCTPThuRpt";


        private Library.Base.IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.AccountPayableRpt.Executor, Module.AccountPayableRpt");
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

        //[HttpPost]
        //[Route("get-thcnpthu")]
        //public IHttpActionResult GetTongHopCongNoPhaiThu(System.Collections.Hashtable filters)
        //{
        //    //authentication
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_CTPThu, Library.DTO.ModuleAction.CanRead))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }
        //    object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-thcnpthu", filters, out Library.DTO.Notification notification);
        //    return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        //}

        [HttpPost]
        [Route("query-client")]
        public IHttpActionResult QuyeryClient(string param)
        {
            Hashtable input = new Hashtable();
            input["query"] = param;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "query-client", input, out notification);
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

        [HttpPost]
        [Route("process")]
        public IHttpActionResult Process(object dtoItem)
        {
            Hashtable input = new Hashtable();
            input["query"] = dtoItem;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "process", input, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = dtoItem, Message = notification });
        }
        
        [HttpPost]
        [Route("getpurchaseinvoice")]
        public IHttpActionResult GetPurchaseInvoiceDTO(System.Collections.Hashtable filters)
        {
            //authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_DetailOfLiabilities, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpurchaseinvoice", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

    }
}