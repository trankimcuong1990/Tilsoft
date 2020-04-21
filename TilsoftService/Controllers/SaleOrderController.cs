using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/SaleOrder")]
    public class SaleOrderController : ApiController
    {

        private string moduleCode = "SaleOrderMng";

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

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            
            DTO.SaleOrderMng.DataSearchContainer searchData = bll.SearchDataContainer(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.DataSearchContainer>() { Data = searchData, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id,int offerID, string orderType = null)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            DTO.SaleOrderMng.DataContainer SaleOrder = bll.GetDataContainer(id, offerID, orderType, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.DataContainer>() { Data = SaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.SaleOrderMng.SaleOrder dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.SaleOrderMng.SaleOrder>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();

            //validate quantity in stock
            if (!bll.ValidateStockQuantity(id, dtoItem, ControllerContext.GetAuthUserId(), out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoItem, Message = notification });
            }
            
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoItem, Message = notification });
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

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("saleorders")]
        public IHttpActionResult SearchSaleOrders(int offerID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            DTO.SaleOrderMng.PIFormContainerDTO data = bll.SearchSaleOrders(offerID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.PIFormContainerDTO>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("saleorderdetails")]
        public IHttpActionResult SearchSaleOrderDetails(int saleOrderID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.SaleOrderMng.SaleOrderDetailSearch> saleOrderDetails = bll.SearchSaleOrderDetails(saleOrderID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.SaleOrderMng.SaleOrderDetailSearch>>() { Data = saleOrderDetails, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("confirm")]
        public IHttpActionResult Confirm(int id, DTO.SaleOrderMng.SaleOrder dtoItem, bool withoutSigned)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            
            bll.Confirm(id, ref dtoItem, ControllerContext.GetAuthUserId(), withoutSigned, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("revise")]
        public IHttpActionResult Revise(int id, DTO.SaleOrderMng.SaleOrder dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            bll.Revise(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("printpi")]
        public IHttpActionResult PrintPI(int id, string reportName)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //GET DATA
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            DTO.SaleOrderMng.PIContainerPrintout dtoPrintout = bll.GetPrintoutData(id, ControllerContext.GetAuthUserId(), out notification);

            //CREATE PRINTOUT
            if (reportName == null)
            {
                reportName = dtoPrintout.ReportName;
            }

            //int? companyID = fwBll.GetCompanyID(ControllerContext.GetAuthUserId());
            //switch (companyID)
            //{
            //    case 13:
            //        reportName = reportName + "_OrangePine.rdlc";
            //        break;
            //    default:
            //        reportName = reportName + ".rdlc";
            //        break;
            //}
            reportName = reportName + ".rdlc";
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + reportName;

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoice.Name = "Order";
            rsInvoice.Value = dtoPrintout.PIPrintouts;
            lr.DataSources.Add(rsInvoice);

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoiceDetail.Name = "OrderDetail";
            rsInvoiceDetail.Value = dtoPrintout.PIDetailPrintouts;
            lr.DataSources.Add(rsInvoiceDetail);

            string printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");

            //
            // author: TheMy
            // description: add disclaimer (using iTextSharp)
            //
            string finalFile = System.Guid.NewGuid().ToString().Replace("-", "") + ".pdf";
            FileInfo fInfo = new FileInfo(FrameworkSetting.Setting.AbsoluteReportFolder + reportName);
            if (File.Exists(FrameworkSetting.Setting.AbsoluteReportFolder + fInfo.Name.Replace(fInfo.Extension, "") + "_bottom.pdf"))
            {
                try
                {
                    Document document = new Document();
                    PdfCopy writer = new PdfCopy(document, new FileStream(FrameworkSetting.Setting.AbsoluteReportTmpFolder + finalFile, FileMode.Create));
                    if (writer == null)
                    {
                        throw new Exception("Can not create Pdf object");
                    }
                    document.Open();
                    writer.AddDocument(new PdfReader(FrameworkSetting.Setting.AbsoluteReportTmpFolder + printoutFileName));
                    writer.AddDocument(new PdfReader(FrameworkSetting.Setting.AbsoluteReportFolder + fInfo.Name.Replace(fInfo.Extension, "") + "_bottom.pdf"));
                    writer.Close();
                    document.Close();
                }
                catch (Exception ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = "Adding general condition failed: " + ex.Message;
                    finalFile = printoutFileName;
                }
            }
            else
            {
                finalFile = printoutFileName;
            }
            //
            //
            //

            return Ok(new Library.DTO.ReturnData<string>() { Data = finalFile, Message = notification });
        }

        [HttpPost]
        [Route("reportorderoverview")]
        public IHttpActionResult GetReportOrderOverview(string season, string orderType)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            string reportFileName = bll.GetReportOrderOverview(season, orderType, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("multi_confirm")]
        public IHttpActionResult MultiConfirm(List<int> ids, bool withoutSigned)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            IEnumerable<int> saleOrderIDSuccess = bll.MultiConfirm(ids, ControllerContext.GetAuthUserId(), withoutSigned, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<int>>() { Data = saleOrderIDSuccess, Message = notification });
        }

        [HttpPost]
        [Route("multi_revise")]
        public IHttpActionResult MultiRevise(List<int> ids)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            IEnumerable<int> saleOrderIDSuccess = bll.MultiRevise(ids, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<int>>() { Data = saleOrderIDSuccess, Message = notification });
        }

        [HttpPost]
        [Route("multi_delete")]
        public IHttpActionResult MultiDelete(List<int> ids)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            IEnumerable<int> saleOrderIDSuccess = bll.MultiDelete(ids, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<int>>() { Data = saleOrderIDSuccess, Message = notification });
        }

        [HttpPost]
        [Route("getloadingplan")]
        public IHttpActionResult GetLoadingPlan(int saleOrderID)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            List<DTO.SaleOrderMng.LoadingPlan> data = bll.GetLoadingPlan(saleOrderID, out notification).ToList();

            return Ok(new Library.DTO.ReturnData<List<DTO.SaleOrderMng.LoadingPlan>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("createreturnorder")]
        public IHttpActionResult CreateReturnData(DTO.SaleOrderMng.SaleOrder dtoItem)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            bool data = bll.CreateReturnData(dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoffertrackingstatus")]
        public IHttpActionResult GetOfferTrackingStatus(int? offerID)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            int? data = bll.GetOfferTrackingStatus(offerID, out notification);
            return Ok(new Library.DTO.ReturnData<int?>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getloadingplan2")]
        public IHttpActionResult GetLoadingPlan2(int saleOrderID)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            List<DTO.SaleOrderMng.LoadingPlan2> data = bll.GetLoadingPlan2(saleOrderID, out notification).ToList();
            return Ok(new Library.DTO.ReturnData<List<DTO.SaleOrderMng.LoadingPlan2>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("returngoods")]
        public IHttpActionResult CreateReturnData(List<DTO.SaleOrderMng.LoadingPlan2> dtoReturns)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            bool data = bll.CreateReturnData2(dtoReturns, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("uploadSignedPIFile")]
        public IHttpActionResult UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer)
        {
            DTO.SaleOrderMng.SaleOrder dtoSaleOrder;
            Library.DTO.Notification notification;
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UploadSignedPIFile(saleOrderID, newFile, oldPointer, out dtoSaleOrder, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoSaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("uploadClientPOFile")]
        public IHttpActionResult UploadClientPOFile(int saleOrderID, string newFile, string oldPointer)
        {
            DTO.SaleOrderMng.SaleOrder dtoSaleOrder;
            Library.DTO.Notification notification;
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UploadClientPOFile(saleOrderID, newFile, oldPointer, out dtoSaleOrder, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoSaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("uploadLCFile")]
        public IHttpActionResult UploadLCFile(int saleOrderID, string newFile, string oldPointer)
        {
            DTO.SaleOrderMng.SaleOrder dtoSaleOrder;
            Library.DTO.Notification notification;
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UploadLCFile(saleOrderID, newFile, oldPointer, out dtoSaleOrder, out notification);

            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.SaleOrder>() { Data = dtoSaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("getviewdata")]
        public IHttpActionResult GetViewData(int id, int offerID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            DTO.SaleOrderMng.DataContainerOverview SaleOrder = bll.GetViewData(id, offerID, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.SaleOrderMng.DataContainerOverview>() { Data = SaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-line")]
        public IHttpActionResult GetOfferLine(int offerId, string orderType, string searchArt)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            List<DTO.SaleOrderMng.SaleOrderDetail> data = bll.GetOfferLine(offerId, orderType, searchArt, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.SaleOrderMng.SaleOrderDetail>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-line-sparepart")]
        public IHttpActionResult GetOfferLineSparepart(int offerId, string orderType, string searchArtSparepart)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            List<DTO.SaleOrderMng.SaleOrderDetailSparepart> data = bll.GetOfferLineSparepart(offerId, orderType, searchArtSparepart, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.SaleOrderMng.SaleOrderDetailSparepart>>() { Data = data, Message = notification });
        }
        
        [HttpPost]
        [Route("get-offer-line-sample")]
        public IHttpActionResult GetOfferLineSample(int offerId, string orderType, string searchSample)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            List<DTO.SaleOrderMng.SaleOrderDetailSample> data = bll.GetOfferLineSample(offerId, orderType, searchSample, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.SaleOrderMng.SaleOrderDetailSample>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("bizbloqs-import-invoice")]
        public IHttpActionResult BizBloqsImportData(List<DTO.SaleOrderMng.BizBloqsInvoice> bizBloqsInvoice)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            List<DTO.SaleOrderMng.BizBloqsInvoice> data = bll.BizBloqsImportData(bizBloqsInvoice, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.SaleOrderMng.BizBloqsInvoice>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("saveldsclient")]
        public IHttpActionResult SaveLDSClient(int saleOrderID, string ldsDate, string remark)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            bll.SaveLDSClient(saleOrderID, ldsDate, remark, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = saleOrderID, Message = notification });
        }
    }
}
