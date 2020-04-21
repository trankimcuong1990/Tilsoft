using Library.Base;
using Library.DTO;
using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/productionFrameWeight")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class ProductionFrameWeightMngController : ApiController
    {
        private readonly string moduleCode = "ProductionFrameWeightMng";
        private readonly IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductionFrameWeightMng.Executor, Module.ProductionFrameWeightMng");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            throw new Exception();
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, TotalRows = totalRows, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            throw new Exception();
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetData", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            if (id == 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id != 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["view"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateData", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteData", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            throw new Exception();
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            throw new Exception();
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(string workOrderUD, string clientUD, string proformaInvoiceNo, string productionItem)
        {
            // authentication
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Notification notification = new Notification();
            Hashtable filters = new Hashtable();

            filters["workOrderUD"] = workOrderUD;
            filters["clientUD"] = clientUD;
            filters["proformaInvoiceNo"] = proformaInvoiceNo;
            filters["productionItem"] = productionItem;
           
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

    }
}
