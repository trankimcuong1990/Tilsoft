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
    [Authorize]
    [RoutePrefix("api/qualityinspectionrpt")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class QualityInspectionRptController : ApiController
    {
        private readonly string moduleCode = "QualityInspectionRpt";
        private readonly string moduleCode_1 = "QualityInspectionTypeMng";
        private readonly string moduleCode_2 = "QualityInspectionCorrectActionMng";
        private readonly string moduleCode_3 = "QualityInspectionDefaultSettingMng";

        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.QualityInspectionRpt.Executor, Module.QualityInspectionRpt");
        private readonly Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getqualityinspectiontype")]
        public IHttpActionResult GetDataSupportType()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_1, ModuleAction.CanRead)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable() { ["typeName"] = "QualityInspectionType" };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getqualityinspectiontype", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatequalityinspectiontype")]
        public IHttpActionResult UpdateDataSupportType(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_1, ModuleAction.CanUpdate)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable()
            {
                ["typeName"] = "QualityInspectionType",
                ["dataView"] = dataView,
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatequalityinspectiontype", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletequalityinspectiontype")]
        public IHttpActionResult DeleteDataSupportType(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_1, ModuleAction.CanDelete)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable()
            {
                ["typeName"] = "QualityInspectionType",
                ["dataView"] = dataView,
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletequalityinspectiontype", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getqualityinspectioncorrectaction")]
        public IHttpActionResult GetDataSupportCorrectAction()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_2, ModuleAction.CanRead)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable() { ["typeName"] = "QualityInspectionCorrectAction" };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getqualityinspectioncorrectaction", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatequalityinspectioncorrectaction")]
        public IHttpActionResult UpdateDataSupportCorrectAction(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_2, ModuleAction.CanUpdate)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable()
            {
                ["typeName"] = "QualityInspectionCorrectAction",
                ["dataView"] = dataView,
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatequalityinspectioncorrectaction", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletequalityinspectioncorrectaction")]
        public IHttpActionResult DeleteDataSupportCorrectAction(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_2, ModuleAction.CanDelete)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable()
            {
                ["typeName"] = "QualityInspectionCorrectAction",
                ["dataView"] = dataView,
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletequalityinspectioncorrectaction", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getqualityinspectiondefaultsetting")]
        public IHttpActionResult GetDataSupportDefaultSetting()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_3, ModuleAction.CanRead)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable() { ["typeName"] = "QualityInspectionDefaultSetting" };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getqualityinspectiondefaultsetting", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatequalityinspectiondefaultsetting")]
        public IHttpActionResult UpdateDataSupportDefaultSetting(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_3, ModuleAction.CanUpdate)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable()
            {
                ["typeName"] = "QualityInspectionDefaultSetting",
                ["dataView"] = dataView,
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatequalityinspectiondefaultsetting", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletequalityinspectiondefaultsetting")]
        public IHttpActionResult DeleteDataSupportDefaultSetting(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode_3, ModuleAction.CanDelete)) { return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED)); }

            Hashtable filters = new Hashtable()
            {
                ["typeName"] = "QualityInspectionDefaultSetting",
                ["dataView"] = dataView,
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletequalityinspectiondefaultsetting", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getqualityinspection")]
        public IHttpActionResult GetData(int id, int? workOrderID, int? clientID, int? productionItemID, string workCenterNM)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["id"] = id,
                ["workOrderID"] = workOrderID,
                ["clientID"] = clientID,
                ["productionItemID"] = productionItemID,
                ["workCenterNM"] = workCenterNM,
                ["typeName"] = "QualityInspection"
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getqualityinspection", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatequalityinspection")]
        public IHttpActionResult UpdateData(object dataView)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["dataView"] = dataView
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatequalityinspection", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletequalityinspection")]
        public IHttpActionResult DeleteData(int id, int? workOrderID, int? clientID, int? productionItemID, string workCenterNM)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["workOrderID"] = workOrderID;
            filters["clientID"] = clientID;
            filters["productionItemID"] = productionItemID;
            filters["workCenterNM"] = workCenterNM;
            filters["typeName"] = "QualityInspection";

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletequalityinspection", filters, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
