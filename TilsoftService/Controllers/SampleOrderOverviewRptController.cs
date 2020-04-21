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
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/sampleorderoverviewrpt")]
    public class SampleOrderOverviewRptController : ApiController
    {
        private string moduleCode = "SampleOrderOverviewRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.SampleOrderOverviewRpt.Executor, Module.SampleOrderOverviewRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead) || fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                Library.DTO.Notification notification = new Library.DTO.Notification();

                // Get permission can edit or view of end-user.
                bool canEdit = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate);
                bool canRead = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead);

                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                inputs["canEdit"] = canEdit;
                inputs["canRead"] = canRead;

                object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getinitdata", inputs, out notification);
                return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
            }
            else
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(string season, string c, int? vnf, int? nlf, int? s, int? so, int? ud)
        {
            // authentication
            if (fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead) || fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // Get permission can edit or view of end-user.
                bool canEdit = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate);
                bool canRead = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead);

                Library.DTO.Notification notification = new Library.DTO.Notification();
                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                inputs["Season"] = season;
                inputs["clientId"] = c;
                inputs["vnFactoryId"] = vnf;
                inputs["nlFactoryId"] = nlf;
                inputs["sampleProductStatusID"] = s;
                inputs["sampleOrderStatusID"] = so;
                inputs["sampleOrderID"] = ud;

                // Add value filter into inputs.
                inputs["canEdit"] = canEdit;
                inputs["canRead"] = canRead;

                string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", inputs, out notification).ToString();
                return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
            }
            else
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
        }

        [HttpPost]
        [Route("getexcelprint")]
        public IHttpActionResult GetExcelPrintData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelprint", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getsampleorder")]
        public IHttpActionResult GetSampleOrder(DTO.Search searchDto)
        {
            if (fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead) || fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                Library.DTO.Notification notification = new Library.DTO.Notification();

                // Add 2 values filter.
                searchDto.Filters["canEdit"] = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate);
                searchDto.Filters["canRead"] = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead);

                object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsampleorder", searchDto.Filters, out notification);

                return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
            }
            else
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
        }

        [HttpPost]
        [Route("getexcelbarcode")]
        public IHttpActionResult GetExportBarCode(string param, string qnt)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["keyIDs"] = param;
            filters["qntBarcode"] = qnt;

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelbarcode", filters, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getexcelreportdatasampleprocess")]
        public IHttpActionResult GetExcelReportDataSampleProcess(string season, string c, int? vnf, int? nlf, int? s, int? so, int? ud)
        {
            // authentication
            if (fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead) || fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // Get permission can edit or view of end-user.
                bool canEdit = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate);
                bool canRead = fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead);

                Library.DTO.Notification notification = new Library.DTO.Notification();
                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                inputs["Season"] = season;
                inputs["clientId"] = c;
                inputs["vnFactoryId"] = vnf;
                inputs["nlFactoryId"] = nlf;
                inputs["sampleProductStatusID"] = s;
                inputs["sampleOrderStatusID"] = so;
                inputs["sampleOrderID"] = ud;

                // Add value filter into inputs.
                inputs["canEdit"] = canEdit;
                inputs["canRead"] = canRead;

                string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreportdatasampleprocess", inputs, out notification).ToString();
                return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
            }
            else
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
        }
    }
}
