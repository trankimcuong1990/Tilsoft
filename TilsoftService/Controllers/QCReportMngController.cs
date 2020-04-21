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
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/qcreportmng")]
    public class QCReportMngController : ApiController
    {
        private string moduleCode = "QCReportMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.QCReportMng.Executor, Module.QCReportMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            var data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinspection")]
        public IHttpActionResult GetInspection(string param)
        {
            Hashtable filters = new Hashtable();
            filters["param"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinspection", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search input)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            input.Filters.Add("UserID", ControllerContext.GetAuthUserId());

            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), input.Filters, input.PageSize, input.PageIndex, input.SortedBy, input.SortedDirection, out int totalRows, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("searchpi")]
        public IHttpActionResult GetMaterialOption(string param, int factoryID)
        {
            Hashtable filters = new Hashtable();
            filters["searchQuery"] = param;
            filters["factoryID"] = factoryID;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "searchpi", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("getData")]
        public IHttpActionResult GetData(int id, int? saleOrderDetailID, int? factoryID, string itemFactoryOrderLink, int? clientID, string articleCode)
        {
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["saleOrderDetailID"] = saleOrderDetailID;
            filters["factoryID"] = factoryID;
            filters["itemFactoryOrderLink"] = itemFactoryOrderLink;
            filters["clientID"] = clientID;
            filters["articleCode"] = articleCode;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object obj)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref obj, out Notification notification);

            return Ok(new ReturnData<object>() { Data = obj, Message = notification });
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

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("get-pdf-report")]
        public IHttpActionResult GetExportPDF(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-pdf-report", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getlistfactoryorder")]
        public IHttpActionResult GetListPIFromFactoryOrderDTO(string articleCode, int? clientID, int? factoryID)
        {
            Hashtable filters = new Hashtable();
            filters["clientID"] = clientID;
            filters["factoryID"] = factoryID;
            filters["articleCode"] = articleCode;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getlistfactoryorder", filters, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }
    }
}
