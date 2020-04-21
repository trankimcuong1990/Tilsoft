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
    [RoutePrefix("api/offer")]
    public class OfferController : ApiController
    {
        private string moduleCode = "OfferMng";        

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

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.OfferMng.OfferSearchResult> result = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

           
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.OfferMng.OfferSearchResult>>() { Data = result, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getsupport")]
        public IHttpActionResult GetSupportData()
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.SupportDataContainer result = bll.GetSupportData(out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.SupportDataContainer>() { Data = result, Message = notification, TotalRows = 0 });
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

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.DataContainer data = bll.GetDataContainer(id, ControllerContext.GetAuthUserId(), out notification);
            //foreach (var item in data.OfferData.OfferLines)
            //{
            //    item.ProductThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + item.ProductThumbnailLocation;
            //    item.ProductFileLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["FileUrl"] + item.ProductFileLocation;
            //}
            
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.DataContainer>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id,int actionType, DTO.OfferMng.Offer dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.OfferMng.Offer>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.OfferMng.Offer>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.OfferMng bll = new BLL.OfferMng();
            bll.UpdateData(id, actionType, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.Offer>() { Data = dtoItem, Message = notification });
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

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("offerlines")]
        public IHttpActionResult SearchOfferLine(int offerID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.OfferMng.OfferLine> offerLineData = bll.SearchOfferLines(offerID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.OfferMng.OfferLine>>() { Data = offerLineData, Message = notification, TotalRows = totalRows });
        }
        

        [HttpPost]
        [Route("confirm")]
        public IHttpActionResult Confirm(int id, int actionType, DTO.OfferMng.Offer dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;

            bll.Confirm(id, actionType, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.Offer>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("revise")]
        public IHttpActionResult Revise(int id, int actionType, DTO.OfferMng.Offer dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;

            bll.Revise(id, actionType, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.Offer>() { Data = dtoItem, Message = notification });
        }


        [HttpPost]
        [Route("offerline-support-edit")]
        public IHttpActionResult GetOfferLineEdit(int offerLineID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.OfferLineEdit data = bll.GetOfferLineEdit(offerLineID, ControllerContext.GetAuthUserId(), out notification);
            data.MaterialThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + data.MaterialThumbnailLocation;
            data.FrameMaterialThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + data.FrameMaterialThumbnailLocation;
            data.SubMaterialColorThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + data.SubMaterialColorThumbnailLocation;
            data.CushionColorThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + data.CushionColorThumbnailLocation;
            data.BackCushionThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + data.BackCushionThumbnailLocation;
            data.SeatCushionThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + data.SeatCushionThumbnailLocation;
            
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.OfferLineEdit>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getprintoffer")]
        public IHttpActionResult GetPrintDataOffer(int offerID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.GetPrintDataOffer(offerID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getprintoffer2")]
        public IHttpActionResult GetPrintDataOffer2(int offerID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.GetPrintDataOffer2(offerID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getprintoffer5")]
        public IHttpActionResult GetPrintDataOffer5(int offerID, bool IsSendEmail, bool IsGetFullSizeImage, int imageOption)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.GetPrintDataOffer5(offerID, IsSendEmail, IsGetFullSizeImage, imageOption, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getprintofferpp2013")]
        public IHttpActionResult GetPrintDataOfferPP2013(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.GetPrintDataOfferPP2013(id, ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }


        [HttpPost]
        [Route("getmodelsparepart")]
        public IHttpActionResult GetModelSparepart(int modelID)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            IEnumerable<DTO.Support.ModelSparepart> data = bll.GetModelSparepart(modelID);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ModelSparepart>>() { Data = data});
        }

        [HttpPost]
        [Route("uploadofferscanfile")]
        public IHttpActionResult UploadOfferScanFile(int offerID, string newOfferScanFile, string offerScanFile)
        {
            DTO.OfferMng.Offer dtoOffer;
            Library.DTO.Notification notification;
            BLL.OfferMng bll = new BLL.OfferMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UploadOfferScanFile(offerID,true,newOfferScanFile,offerScanFile,out dtoOffer, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.Offer>() { Data = dtoOffer, Message = notification });
        }

        [HttpPost]
        [Route("getModelInfo")]
        public IHttpActionResult GetModelInfo(int modelID)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.Model data = bll.GetModelInfo(modelID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.Model>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("getviewdata")]
        public IHttpActionResult GetViewData(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.DataContainerOverView data = bll.GetViewData(id, ControllerContext.GetAuthUserId(), out notification);
            foreach (var item in data.OfferData.OfferLines)
            {
                item.ProductThumbnailLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["ThumbnailUrl"] + item.ProductThumbnailLocation;
                item.ProductFileLocation = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"] + System.Configuration.ConfigurationManager.AppSettings["FileUrl"] + item.ProductFileLocation;
            }

            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.DataContainerOverView>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexportexcel")]
        public IHttpActionResult GetExportNewVersion(int offerID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.GetExportNewVersion(offerID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("quick-search-sample-product")]
        public IHttpActionResult QuickSearchSampleProduct(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.OfferMng.SampleProductSearchResultDTO> result = bll.QuickSearchSampleProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);


            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.OfferMng.SampleProductSearchResultDTO>>() { Data = result, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearcharticlecode")]
        public IHttpActionResult QuickSearchArticleCode(DTO.Search searchInput)
        {
            BLL.Support bll = new BLL.Support();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.Support.ModelSeason> data = bll.QuickSearchArticleCode(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.Support.ModelSeason>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get-planing-purchasing-price")]
        public IHttpActionResult GetPlaningPurchasingPrice(int id)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            List<DTO.OfferMng.PlaningPurchasingPriceDTO> Data = bll.GetPlaningPurchasingPrice(ControllerContext.GetAuthUserId(), id, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<List<DTO.OfferMng.PlaningPurchasingPriceDTO>>() { Data = Data, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-line-sale-price-history")]
        public IHttpActionResult GetOfferLineSalePriceHistory(int id, string season)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            DTO.OfferMng.PriceHistory Data = bll.GetOfferLineSalePriceHistory(ControllerContext.GetAuthUserId(), season, id, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.OfferMng.PriceHistory>() { Data = Data, Message = notification });
        }

        [HttpPost]
        [Route("manager-approve")]
        public IHttpActionResult ManagerApprove(int id)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            bool result = bll.ManagerApprove(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("approve-item")]
        public IHttpActionResult ApproveItem(int id)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            bool result = bll.ApproveItem(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("un-approve-item")]
        public IHttpActionResult UnApproveItem(int id)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            bool result = bll.UnApproveItem(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("approve-all-item")]
        public IHttpActionResult ApproveAllItem(int id)
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            bool result = bll.ApproveAllItem(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("get-planing-purchasing-price-2")]
        public IHttpActionResult GetPlaningPurchasingPrice2(object param) // param: DTO.OfferMng.PlanningPurchasingPriceParamDTO
        {
            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;
            List<DTO.OfferMng.PlaningPurchasingPriceDTO> Data = bll.GetPlaningPurchasingPrice(ControllerContext.GetAuthUserId(), param, out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<List<DTO.OfferMng.PlaningPurchasingPriceDTO>>() { Data = Data, Message = notification });
        }

        [HttpPost]
        [Route("get-fobitemonly")]
        public IHttpActionResult GetExcelFOBItemOnlyReportData(int offerID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.GetExcelFOBItemOnlyReportData(offerID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("excel-searchoffer")]
        public IHttpActionResult GetExcelFOBItemOnlyReportData(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.ExportExcelSearchOffer(searchInput.Filters, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("send-nofitication-approve-offer")]
        public IHttpActionResult SendEmailNotificationByApproveOfferPermission(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            Library.DTO.Notification notification;

            object data = bll.SendEmailNotificationByApproveOfferPermission(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("export-detail")]
        public IHttpActionResult ExportDetail(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OfferMng bll = new BLL.OfferMng();
            string reportFileName = bll.ExportOfferDetailToExcel(id, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }
    }
}
