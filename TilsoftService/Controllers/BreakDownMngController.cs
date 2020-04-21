using System;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/breakdown")]
    public class BreakDownMngController : ApiController
    {
        private string moduleCode = "BreakDownMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.BreakDownMng.Executor, Module.BreakDownMng");
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
        public IHttpActionResult Get(int id, int modelId, int sampleProductId, int offerSeasonDetailId, int? factoryId)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            input["modelId"] = modelId;
            input["sampleProductId"] = sampleProductId;
            input["offerSeasonDetailId"] = offerSeasonDetailId;
            if (factoryId.HasValue)
            {
                input["factoryId"] = factoryId;
            }
            else
            {
                input["factoryId"] = 0;
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //
        // CUSTOM FUNCTIONS
        //
        [HttpPost]
        [Route("get-product-option")]
        public IHttpActionResult GetProductOption(int categoryId, int modelId, int productGroupId)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["categoryId"] = categoryId;
            input["modelId"] = modelId;
            input["productGroupId"] = productGroupId;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getproductoption", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update-option")]
        public IHttpActionResult UpdateOption(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            input["data"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatecategoryoption", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpGet]
        [Route("quick-search-production-item")]
        public IHttpActionResult QuickSearchProductionItem(string query, string type)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["query"] = query;
            input["type"] = type;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchproductionitem", input, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("getModel")]
        public IHttpActionResult GetModel(string param)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getModel", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSampleProduct")]
        public IHttpActionResult GetSampleProduct(string param)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getSampleProduct", filters, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-price")]
        public IHttpActionResult GetPrice(object dtoItem, int id, string articleCode)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["data"] = dtoItem;
            input["id"] = id;
            input["articleCode"] = articleCode;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getprice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("get-articlecode")]
        public IHttpActionResult GetArticleCode(object dtoItem, int modelID, int offerSeasonDetailID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["data"] = dtoItem;           
            input["modelID"] = modelID;
            input["offerSeasonDetailID"] = offerSeasonDetailID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-articlecode", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
        
        [HttpPost]
        [Route("update-price")]
        public IHttpActionResult UpdatePrice(object dtoItem, int id, string articleCode)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["data"] = dtoItem;
            input["id"] = id;
            input["articleCode"] = articleCode;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updateprice", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve-option")]
        public IHttpActionResult ApproveOption(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "approve-option", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("un-approve-option")]
        public IHttpActionResult UnApproveOption(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "un-approve-option", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve-all-option")]
        public IHttpActionResult ApproveAllOption(object dtoItem)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["data"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "approve-all-option", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("un-approve-all-option")]
        public IHttpActionResult UnApproveAllOption(object dtoItem)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["data"] = dtoItem;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "un-approve-all-option", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update-season-spec")]
        public IHttpActionResult UpdateSeasonSpec(int id, decimal percent)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            input["data"] = percent;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-season-spec", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-default-production-item")]
        public IHttpActionResult GetDefaultProductionItem(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-default-production-item", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcaculation")]
        public IHttpActionResult GetCaculation()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();          
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getcaculation", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("import-bom-data")]
        public IHttpActionResult ImportBOMData()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "import-bom-data", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("import-bom-data-single")]
        public IHttpActionResult ImportBOMDataSingle(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "import-bom-data-single", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("export-excel-breakdown")]
        public IHttpActionResult GetExportToExcelBreakdown(DTO.Search input)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "export-to-excel", input.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-purchasing-price")]
        public IHttpActionResult GetPurchasingPriceData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["id"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-purchasing-price", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getfilterdata")]
        public IHttpActionResult GetFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getfilterdata", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPriceQuotation")]
        public IHttpActionResult GetPriceQuotation(object dtoItem)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["data"] = dtoItem;            
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpricequotation", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
