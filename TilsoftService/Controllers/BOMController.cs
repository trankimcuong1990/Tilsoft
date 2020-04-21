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
    [RoutePrefix("api/BOM")]
    public class BOMController : ApiController
    {
        private string moduleCode = "BOMMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BOM.Executor, Module.BOM");
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
        [Route("get")]
        public IHttpActionResult Get(int id,int? workOrderID)
        {
            Library.DTO.Notification notification;
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;
            
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(new Exception(Properties.Resources.NOT_AUTHORIZED)).Message;

                return Ok(new Library.DTO.ReturnData<object>() { Data = null, Message = notification });
            }
            
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["id"] = id;
            filters["workOrderID"] = workOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
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
        [Route("updateProductionItem")]
        public IHttpActionResult UpdateProductionItem(int productionItemID, object dtoItem)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionItemID"] = productionItemID;
            filters["dtoItem"] = dtoItem;

            //set indenfier is tempFolder which to use to save image
            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateProductionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getProductionItem")]
        public IHttpActionResult GetProductionItem(int productionItemID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionItemID"] = productionItemID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("getListProductionItem")]
        public IHttpActionResult GetListProductionItem(object bomImportData)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["bomImportData"] = bomImportData;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetListProductionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("createImportTemplateGeneral")]
        public IHttpActionResult CreateImportTemplateGeneral(int workOrderID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["workOrderID"] = workOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateImportTemplateGeneral", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("createImportTemplate")]
        public IHttpActionResult CreateImportTemplate(int workOrderID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["workOrderID"] = workOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateImportTemplate", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("create-bom-template-data")]
        public IHttpActionResult CreateBOMTemplateData(int workOrderID, int? copyFromProductionProcessID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["workOrderID"] = workOrderID;
            filters["copyFromProductionProcessID"] = copyFromProductionProcessID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateBOMTemplateData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("set-default-workorder")]
        public IHttpActionResult SetDefaultWorkOrder(int workOrderID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["workOrderID"] = workOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "SetDefaultWorkOrder", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     -  get list production process
        ///     -  every production process have one BOM Standard
        ///     - That mean show all BOM Standard so user can use it to copy and make new BOM to apply to workOrder
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-production-process-form-data")]
        public IHttpActionResult GetProductionProcessFormData(int modelID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["modelID"] = modelID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionProcessFormData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
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
        [Route("exportbomtoexcel")]
        public IHttpActionResult ExportBOMStandardToExcel(int bomID, Hashtable param)
        {
            Library.DTO.Notification notification;
            param["bomID"] = bomID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ExportBOMToExcel", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
