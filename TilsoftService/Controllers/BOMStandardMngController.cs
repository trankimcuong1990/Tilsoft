using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/bomstandard")]
    public class BOMStandardMngController : ApiController
    {
        private string moduleCode = "BOMStandardMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BOMStandardMng.Executor, Module.BOMStandardMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        /// <summary>
        ///     get BOM Standard
        /// </summary>
        /// <param name="id">bom standard id</param>
        /// <param name="param">
        ///     productionProcessID = {
        ///         productionProcessID: value
        ///     }
        /// </param>
        /// <returns>
        ///     return bom object
        /// </returns>
        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, Hashtable param)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            param["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetData", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     update BOM Standard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoItem"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     delete BOM Standard and also delete production process
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     auto create productionItem basen on bom data imported and get back list this productionItem to frontend to use for create bom standard
        /// </summary>
        /// <param name="bomImportData">bom input data from excel</param>
        /// <returns>list production item</returns>
        [HttpPost]
        [Route("get-list-production-item")]
        public IHttpActionResult GetListProductionItem(object bomImportData)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["bomImportData"] = bomImportData;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetListProductionItem", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        /// <summary>
        ///     create excel file BOM template base on workcenter of OPSequence and pieces of product. So user can fill-in material for these component and they can use this file to import back to BOM
        /// </summary>
        /// <param name="productionProcessID">process of one production, it containt OPSequence & Product</param>
        /// <returns>excel file</returns>
        [HttpPost]
        [Route("create-import-template")]
        public IHttpActionResult CreateImportTemplate(int productionProcessID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionProcessID"] = productionProcessID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateImportTemplate", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     get data service for create BOM base on OP Sequence and pieces of product and material from bom selected to copy
        /// </summary>
        /// <param name="productionProcessID">process of one production, it containt OPSequence & Product</param>
        /// <param name="copyFromproductionProcessID">source process, so system can take material from BOM in this process</param>
        /// <returns>info data about component, piece, workcenter, bomsource and fronted use to make bom and show in html</returns>
        [HttpPost]
        [Route("create-bom-template-data")]
        public IHttpActionResult CreateBOMTemplateData(int productionProcessID, int? copyFromproductionProcessID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionProcessID"] = productionProcessID;
            filters["copyFromproductionProcessID"] = copyFromproductionProcessID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "CreateBOMTemplateData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     every production process for every product, so we need set default for production process that have same modelID
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("set-bom-standard-default")]
        public IHttpActionResult SetBOMStandardDefault(int productionProcessID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionProcessID"] = productionProcessID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "SetBOMStandardDefault", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     get list all production process that are same model
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-production-process-by-model")]
        public IHttpActionResult GetProductionProcessByModel(int modelID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["modelID"] = modelID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionProcessByModel", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     get production process
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-production-process")]
        public IHttpActionResult GetProductionProcessData(int productionProcessID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionProcessID"] = productionProcessID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionProcessData", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     get production process
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getViewData")]
        public IHttpActionResult GetViewData(int id, Hashtable param)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            param["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetViewData", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     get production process
        /// </summary>
        /// <param name="productionProcessID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-production-process")]
        public IHttpActionResult UpdateProductionProcess(int productionProcessID, object dtoData)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["productionProcessID"] = productionProcessID;
            filters["dtoData"] = dtoData;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateProductionProcess", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        /// <summary>
        ///     - search production process. Production process is table containt product and OPSequence, so we can make bom base on production process
        ///     - every production process corresponding with one product and corresponding with one BOM
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("search-production-process")]
        public IHttpActionResult SearchProductionProcess(DTO.Search searchInput)
        {
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
        [Route("export-bomstandard-to-excel")]
        public IHttpActionResult ExportBOMStandardToExcel(int bomStandardID)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["bomStandardID"] = bomStandardID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ExportBOMStandardToExcel", filters, out notification);
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
        [Route("get-work-order-by-id")]
        public IHttpActionResult GetWorkOrderByBOMStandardID(int id)
        {
            Hashtable filters = new Hashtable();
            filters["bomStandardID"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkOrderByBOMStandardID", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-work-order-apply-by-id")]
        public IHttpActionResult GetWorkOrderApplyByBOMStandardID(int id)
        {
            Hashtable filters = new Hashtable();
            filters["bomStandardID"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkOrderApplyByBOMStandardID", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-work-center-by-id")]
        public IHttpActionResult GetWorkCenterByBOMStandardID(int id)
        {
            Hashtable filters = new Hashtable();
            filters["bomStandardID"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetWorkCenterByBOMStandardID", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("apply-bom-standard")]
        public IHttpActionResult ApplyBOMStandard(int id, string workOrderIDs, string workCenterIDs, int applyTypeID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["workOrderIDs"] = workOrderIDs;
            filters["workCenterIDs"] = workCenterIDs;
            filters["applyTypeID"] = applyTypeID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "applyBOMStandard", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
