using Library.DTO;
using System;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/cashbookrpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class CashBookRptController : ApiController
    {
        private string moduleCode = "CashBookRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.CashBookRpt.Executor, Module.CashBookRpt");
        private Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getdatawithfilters")]
        public IHttpActionResult GetSearchFilterData(int paymentType, int? bankAccount, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["paymentType"] = paymentType;
            filters["bankAccount"] = bankAccount;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdatawithfilterexcel")]
        public IHttpActionResult GetSearchFilterDataExcel(int paymentType, int? bankAccount, string startDate, string endDate)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["paymentType"] = paymentType;
            filters["bankAccount"] = bankAccount;
            filters["startDate"] = startDate;
            filters["endDate"] = endDate;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "excel-getsearchfilter", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            // Check permission end-user.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);

            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

    }
}