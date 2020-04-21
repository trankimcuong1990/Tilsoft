using System;
using System.Collections;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/offer-season")]
    [TilsoftService.Lib.LogFilterAtrribute]
    public class OfferSeasonMngController : ApiController
    {
        private string moduleCode = "OfferSeasonMng";
        private Library.Base.IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.OfferSeasonMng.Executor, Module.OfferSeasonMng");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

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
        public IHttpActionResult Get(int id, Hashtable param)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, param, out Library.DTO.Notification notification);
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
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
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
            object data = executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            executor.Approve(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, object dtoItem)
        {
            // authentication
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            executor.Reset(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            object data = executor.GetSearchFilter(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //
        //customize function
        //

        [HttpPost]
        [Route("get-offer-season-detail")]
        public IHttpActionResult GetOfferSeasonDetail(int offerSeasonDetailID)
        {
            Hashtable input = new Hashtable();
            input["offerSeasonDetailID"] = offerSeasonDetailID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-offer-season-detail", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update-offer-season-detail")]
        public IHttpActionResult UpdateOfferSeasonDetail(int offerSeasonID, int offerSeasonDetailID, object dtoOfferSeasonDetail)
        {
            // authentication
            if (offerSeasonDetailID > 0)
            {
                if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }
            }
            else {
                if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }
            }
            //update
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["offerSeasonID"] = offerSeasonID;
            input["offerSeasonDetailID"] = offerSeasonDetailID;
            input["dtoOfferSeasonDetail"] = dtoOfferSeasonDetail;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-offer-season-detail", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("search-model")]
        public IHttpActionResult SearchModel(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-model", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("search-product")]
        public IHttpActionResult SearchProduct(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-product", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("search-sparepart")]
        public IHttpActionResult SearchSparepart(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-sparepart", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-season-type")]
        public IHttpActionResult GetOfferSeasonType()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-offer-season-type", null, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-item-properties")]
        public IHttpActionResult GetOfferItemProperies(Hashtable param)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-offer-item-properties", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-item-default-properties")]
        public IHttpActionResult GetOfferItemDefaultProperies(int modelID, string season)
        {
            Hashtable param = new Hashtable();
            param["modelID"] = modelID;
            param["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-offer-item-default-properties", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("search-client")]
        public IHttpActionResult SearchClient(string param)
        {
            Hashtable input = new Hashtable();
            input["searchQuery"] = param;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-client", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("search-model-sparepart")]
        public IHttpActionResult SearchModelSparepart(int modelID)
        {
            Hashtable param = new Hashtable();
            param["modelID"] = modelID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-model-sparepart", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-planning-purchasing-price")]
        public IHttpActionResult GetPlanningPurchasingPrice(Hashtable param)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-planning-purchasing-price", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update-offer-season-detail2")]
        public IHttpActionResult UpdateOfferSeasonDetail2(int offerSeasonID, object offerSeasonDetails)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            //update
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["offerSeasonID"] = offerSeasonID;
            input["offerSeasonDetails"] = offerSeasonDetails;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "update-offer-season-detail2", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("delete-offer-season-detail")]
        public IHttpActionResult DeleteOfferSeasonDetail(int offerSeasonDetailID)
        {
            Hashtable input = new Hashtable();
            input["offerSeasonDetailID"] = offerSeasonDetailID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "delete-offer-season-detail", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("export-detail")]
        public IHttpActionResult ExportDetail(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["offerSeasonID"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "export-offer-season-detail", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-sale-price-table")]
        public IHttpActionResult GetSalePriceTable(Hashtable param)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-sale-price-table", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-sale-price-table-last-season")]
        public IHttpActionResult GetSalePriceTableLastSeason(int offerSeasonID)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonID"] = offerSeasonID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-sale-price-table-last-season", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-offerline-by-offer-season-detail")]
        public IHttpActionResult GetOfferLineByOfferSeasonDetail(int offerSeasonDetailID)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonDetailID"] = offerSeasonDetailID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-offerline-by-offer-season-detail", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("admin-update-sale-price")]
        public IHttpActionResult AdminUpdateSalePrice(int offerSeasonDetailID, decimal? salePrice)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonDetailID"] = offerSeasonDetailID;
            param["salePrice"] = salePrice;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "admin-update-sale-price", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-image-gallery")]
        public IHttpActionResult GetImageGallery(int offerSeasonID)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonID"] = offerSeasonID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-image-gallery", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("create-offer-season-sample")]
        public IHttpActionResult CreateOfferSeasonSample(int? offerSeasonDetailID, int? clientID, string season)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonDetailID"] = offerSeasonDetailID;
            param["clientID"] = clientID;
            param["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "create-offer-season-sample", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-related-factory-order-detail")]
        public IHttpActionResult GetRelatedFactoryOrderDetail(int offerSeasonID)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonID"] = offerSeasonID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-related-factory-order-detail", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-purchasing-price-last-year")]
        public IHttpActionResult GetPurchasingPriceLastYear(int offerSeasonID)
        {
            Hashtable param = new Hashtable();
            param["offerSeasonID"] = offerSeasonID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-purchasing-price-last-year", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
