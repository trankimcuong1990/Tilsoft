using Library.DTO;
using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{

    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/PurchaseQuotationMng")]
    public class PurchaseQuotationMngController : ApiController
    {
        private string ModuleCode = "PurchaseQuotationMng";
        readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.PurchaseQuotationMng.Executor, Module.PurchaseQuotationMng");
        readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        #region ** Code developer Luu Nhut **

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            searchInput.Filters.Add("userId", ControllerContext.GetAuthUserId());
            var data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });

        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable filters = new System.Collections.Hashtable();

            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, Library.DTO.ModuleAction.CanCreate))
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
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        #endregion

        #region ** Code developer Truong Son **

        [HttpPost]
        [Route("getDefaultPrice")]
        public IHttpActionResult GetDefaultPrice(DTO.Search input)
        {
            //if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}

            //SetModuleIdenfitier(executor);

            //input.Filters.Add("UserID", ControllerContext.GetAuthUserId());
            var data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getDefaultPrice", null, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        private bool HasNotUpdatePermission(int id, out IHttpActionResult httpActionResult)
        {
            httpActionResult = null;

            if (id == 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanCreate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            if (id > 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanUpdate))
            {
                httpActionResult = InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                return true;
            }

            return false;

        }

        #endregion

        [HttpPost]
        [Route("preparing-default-price-data")]
        public IHttpActionResult PreparingDefaultPriceData()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "PreparingDefaultPriceData", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("set-default-price")]
        public IHttpActionResult SetDefaultPrice(string productionItem, int supplier, bool? isDefault, object dtoItem)
        {
            Hashtable param = new Hashtable();
            param["PurchasingDetail"] = dtoItem;
            param["productionItemUD"] = productionItem;
            param["factoryRawMaterialID"] = supplier;
            param["yesNoValue"] = isDefault;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "SetDefaultPrice", param, out Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult ApproveData(int id, object dtoItem)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Notification notification);
            return Ok(new ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getdefaultpriceproductionitem")]
        public IHttpActionResult GetProductionItemDefaultPrice(DTO.Search param)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //Hashtable filters = new Hashtable();
            //filters["searchString"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetProductionItemDefaultPrice", param.Filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdefaultprice")]
        public IHttpActionResult GetInitDefaultPrice()
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetInitDataDefaultPrice", null, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSearchDefaultPrice")]
        public IHttpActionResult GetSearchDefaultPrice(DTO.Search inputSearch)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "PreparingDefaultPriceData", inputSearch.Filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportexcel")]
        public IHttpActionResult GetContentPurchasingPriceFactory(DTO.Search filters)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), ModuleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            filters.Filters["orderBy"] = filters.SortedBy;
            filters.Filters["orderDirection"] = filters.SortedDirection;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "exportexcel", filters.Filters, out Library.DTO.Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getInitForm")]
        public IHttpActionResult GetInitForm()
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), "PurchasingPriceFactoryRpt", ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetInitForm", null, out Notification notification);

            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getMaterial")]
        public IHttpActionResult GetMaterial(string param)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), "PurchasingPriceFactoryRpt", Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetMaterial", input, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPurchasingPriceFactory")]
        public IHttpActionResult GetPurchasingPriceFactory(DTO.Search filters)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), "PurchasingPriceFactoryRpt", Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            int totalRows = 0;
            filters.Filters["totalRows"] = totalRows;
            filters.Filters["pageIndex"] = filters.PageIndex;
            filters.Filters["pageSize"] = filters.PageSize;
            filters.Filters["orderBy"] = filters.SortedBy;
            filters.Filters["orderDirection"] = filters.SortedDirection;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetPurchasingPriceFactory", filters.Filters, out Library.DTO.Notification notification);
            totalRows = (filters.Filters.ContainsKey("totalRows") && filters.Filters["totalRows"] != null && !string.IsNullOrEmpty(filters.Filters["totalRows"].ToString().Trim())) ? Convert.ToInt32(filters.Filters["totalRows"].ToString().Trim()) : 0;

            return Ok(new ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("cancel")]
        public IHttpActionResult Cancel(int id, object dtoItem)
        {
            Hashtable filters = new Hashtable();
            filters["id"] = id;
            filters["dtoItem"] = dtoItem;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "cancel", filters, out Notification notification);

            return Ok(new ReturnData<object> { Data = dtoItem, Message = notification });
        }
    }
}