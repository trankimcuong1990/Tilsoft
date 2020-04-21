using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/ps")]
    public class PSRptController : ApiController
    {
        private string moduleCode = "PSRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PSRpt.Executor, Module.PSRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

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
        [Route("getexcelreportproduct")]
        public IHttpActionResult GetExcelReportDataProduct(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreportproduct", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("export-excel-factoryorderdetail")]
        public IHttpActionResult GetExcelReport2FactoryOrderDetail(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelfactoryorderdetail", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("export-excel-product")]
        public IHttpActionResult GetExcelReport2Product(int id)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelproduct", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
