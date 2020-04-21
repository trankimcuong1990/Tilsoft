using System;
using System.Collections.Generic;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        private string moduleCode = "ClientMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ClientMng.SearchFormData searchResult = bll.SearchClient(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.SearchFormData>() { Data = searchResult, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            DTO.ClientMng.DataContainer data = bll.GetEditData(id, ControllerContext.GetAuthUserId(), out notification);

            //foreach (var item in data.Client.ClientContracts)
            //{
            //    if (!string.IsNullOrEmpty(item.ContractFileURL))
            //    {
            //        item.ContractFileURL = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["FileUrl"] + item.ContractFileURL;
            //    }
            //}
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.DataContainer>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ClientMng.Client dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
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

            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.ClientMng.Client>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.ClientMng.Client>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.ClientMng bll = new BLL.ClientMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);


            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.Client>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getcisreport")]
        public IHttpActionResult GetCISReport(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            string dataFileName = bll.PrintCIS(id, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelclient")]
        public IHttpActionResult ExportExcelClient(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            string dataFileName = bll.ExportExcelClient(ControllerContext.GetAuthUserId(), searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilter()
        {
            BLL.ClientMng bll = new BLL.ClientMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ClientMng.SearchFilterData data = bll.GetSearchFilter(out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        // Custom function to get client overview
        [HttpPost]
        [Route("getclientoverview")]
        public IHttpActionResult GetClientOverview(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            DTO.ClientMng.ClientOverview data = bll.GetClientOverview(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("savegic")]
        public IHttpActionResult SaveGIC(int id, List<DTO.ClientMng.ClientContactHistory> dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            bll.SaveGIC(id, dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ClientMng.ClientContactHistory>>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getclientoverview2")]
        public IHttpActionResult GetClientOverview2(int id, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetDetailClientOverview(id, season, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("setactivestatus")]
        public IHttpActionResult SetActiveStatus(int id, int st)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // continue processing
            BLL.ClientMng bll = new BLL.ClientMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bool result = bll.SetActivateStatus(ControllerContext.GetAuthUserId(), id, st == 1 ? true : false, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }






        [HttpPost]
        [Route("getofferdata")]
        public IHttpActionResult GetOfferData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetOfferData(id, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getpidata")]
        public IHttpActionResult GetPIData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetPIData(id, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getplcdata")]
        public IHttpActionResult GetPLCData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetPLCData(id, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcidata")]
        public IHttpActionResult GetCIData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetCIData(id, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getsampleorderdata")]
        public IHttpActionResult GetSampleOrderData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetSampleOrderData(id, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getclientcomplaindata")]
        public IHttpActionResult GetClientComplainData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();

            // Check authentication.
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            DTO.ClientMng.ClientOverview2 data = bll.GetClientComplainData(id, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.ClientOverview2>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getOfferLine")]
        public IHttpActionResult GetOfferLine(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetOfferLine(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSaleOrderDetail")]
        public IHttpActionResult GetSaleOrderDetail(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetSaleOrderDetail(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getECommercialInvoiceDetail")]
        public IHttpActionResult GetECommercialInvoiceDetail(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetECommercialInvoiceDetail(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getWarehouseInvoiceProductDetail")]
        public IHttpActionResult GetWarehouseInvoiceProductDetail(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetWarehouseInvoiceProductDetail(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getECommercialInvoiceExtDetail")]
        public IHttpActionResult GetECommercialInvoiceExtDetail(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetECommercialInvoiceExtDetail(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("createeurofarstockaccount")]
        public IHttpActionResult CreateEurofarstockAccount(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.CreateEurofarstockAccount(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcisdata")]
        public IHttpActionResult GetCISData(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetCISData(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcisshippingperformance")]
        public IHttpActionResult GetCISShippingPerformance(int id, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetCISShippingPerformance(id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcisitem")]
        public IHttpActionResult GetCISItem(int id, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetCISItem(id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcispricecomparison")]
        public IHttpActionResult GetCISPriceComparison(int id, string season, string seasonToCompare)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetCISPriceComparison(id, season, seasonToCompare, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getloadingplan")]
        public IHttpActionResult GetLoadingPlan(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetLoadingPlan(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdeltadata")]
        public IHttpActionResult GetDeltaData(int id, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetDeltaData(ControllerContext.GetAuthUserId(), id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdelta2data")]
        public IHttpActionResult GetDelta2Data(int id, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetDelta2Data(ControllerContext.GetAuthUserId(), id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getdelta3data")]
        public IHttpActionResult GetDelta3Data(int clientID, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetDelta3Data(ControllerContext.GetAuthUserId(), clientID, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        //
        // SEARCH PI
        //
        [HttpPost]
        [Route("quicksearchpibyarticlecode")]
        public IHttpActionResult QuickSearchPIByArticleCode(int id, string season, string art)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.QuickSearchPI_ByArticleCode(art, id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchpibycommercialinvoice")]
        public IHttpActionResult QuickSearchPIByCommercialInvoice(int id, string season, int ciid)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.QuickSearchPI_ByCommercialInvoice(ciid, id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchpibyloadingplan")]
        public IHttpActionResult QuickSearchPIByLoadingPlan(int id, string season, int lpid)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.QuickSearchPI_ByLoadingPlan(lpid, id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getbreakdownwithfilter")]
        public IHttpActionResult GetBreakdownWithFilter(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            //if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}
            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ClientMng.Overview.Breakdown.SearchFormDTO searchResult = bll.BreakdownGetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.Overview.Breakdown.SearchFormDTO>() { Data = searchResult, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getbreakdownsearchfilter")]
        public IHttpActionResult GetBreakdownSearchFilterData()
        {
            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            DTO.ClientMng.Overview.Breakdown.SupportDataDTO dataResult = bll.BreakdownGetSearchFilter(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.Overview.Breakdown.SupportDataDTO>() { Data = dataResult, Message = notification });
        }

        [HttpPost]
        [Route("getcismodel")]
        public IHttpActionResult GetCISModel(int id, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetCISModel(id, season, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getclientestimatedadditionalcost")]
        public IHttpActionResult GetClientEstimatedAdditionalCost(int id)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = bll.GetClientEstimatedAdditionalCost(id, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getofferoverviewdata")]
        public IHttpActionResult GetOfferOverviewData(int id, string season)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            DTO.ClientMng.Overview.Offer.DataContainer data = bll.GetOfferOverviewData(id, season, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.Overview.Offer.DataContainer>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoffermargindetail")]
        public IHttpActionResult getOfferMarginDetail(int id, string season, int status, int clientID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO> data = bll.GetOfferMarginDetail(id, season, status, clientID, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.ClientMng.Overview.Offer.OfferMarginDetailDTO>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("delta-export-excel")]
        public IHttpActionResult DeltaExportExcel(int clientId, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            string dataFileName = bll.DetalExportExcel(clientId, season, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("delta2-export-excel")]
        public IHttpActionResult Delta2ExportExcel(int clientId, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            string dataFileName = bll.Detal2ExportExcel(clientId, season, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("delta3-export-excel")]
        public IHttpActionResult Delta3ExportExcel(int clientId, string season)
        {
            Library.DTO.Notification notification;
            BLL.ClientMng bll = new BLL.ClientMng();
            string dataFileName = bll.Detal3ExportExcel(clientId, season, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("update-offer-potential-status")]
        public IHttpActionResult UpdateOfferPotentialStatus(List<DTO.ClientMng.Overview.Offer.OfferMarginDTO> dtoItems)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), "OfferMng", Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // continue processing
            BLL.ClientMng bll = new BLL.ClientMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bool result = bll.UpdateOfferPotentialStatus(ControllerContext.GetAuthUserId(), dtoItems, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-planing-purchasing-price")]
        public IHttpActionResult GetPlaningPurchasingPrice(int id)
        {
            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            List<DTO.ClientMng.PlaningPurchasingPriceDTO> Data = bll.GetPlaningPurchasingPrice(ControllerContext.GetAuthUserId(), id, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<List<DTO.ClientMng.PlaningPurchasingPriceDTO>>() { Data = Data, Message = notification });
        }

        [HttpPost]
        [Route("getdataclientadditionalcondition")]
        public IHttpActionResult GetDataClientAdditionalCondition()
        {
            BLL.ClientMng bll = new BLL.ClientMng();
            Library.DTO.Notification notification;
            DTO.ClientMng.DataContainer dataResult = bll.GetDataClientAdditionalCondition(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ClientMng.DataContainer>() { Data = dataResult, Message = notification });
        }
    }
}
