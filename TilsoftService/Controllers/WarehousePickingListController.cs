using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using TilsoftService.Lib;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/warehousepickinglist")]
    public class WarehousePickingListController : ApiController, Lib.IAPIController<DTO.WarehousePickingListMng.WarehousePickingList>
    {
        public string getModuleCode()
        {
            return "WarehousePickingListMng";
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.WarehousePickingListMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehousePickingListMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            Library.DTO.Notification notification;
            DTO.WarehousePickingListMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehousePickingListMng.EditFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.WarehousePickingListMng.WarehousePickingList>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehousePickingListMng.WarehousePickingList>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        public IHttpActionResult Approve(int id, DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Reset(int id, DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        [HttpPost]
        [Route("changestatus")]
        public IHttpActionResult ChangeStatus(int id, int statusId, DTO.WarehousePickingListMng.WarehousePickingList dtoItem)
        {
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            Library.DTO.Notification notification;
            bll.ChangeStatus(id, statusId, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehousePickingListMng.WarehousePickingList>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchremainproduct")]
        public IHttpActionResult QuickSearchRemainProduct(DTO.Search searchInput)
        {
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.WarehousePickingListMng.RemainReservation> data = bll.QuickSearchRemainProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<List<DTO.WarehousePickingListMng.RemainReservation>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchremainsparepart")]
        public IHttpActionResult QuickSearchRemainSparepart(DTO.Search searchInput)
        {
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.WarehousePickingListMng.RemainSparepart> data = bll.QuickSearchRemainSparepart(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<List<DTO.WarehousePickingListMng.RemainSparepart>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        //[HttpPost]
        //[Route("printpickinglist")]
        //public IHttpActionResult PrintPickingList(int id)
        //{
        //    Library.DTO.Notification notification;

        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanPrint))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }

        //    //GET DATA
        //    BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
        //    DTO.WarehousePickingListMng.PickingListContainerPrintout dtoPrintout = bll.GetPickingListPrintoutData(id, ControllerContext.GetAuthUserId(), out notification);

        //    //CREATE PRINTOUT
        //    Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
        //    lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + "PickingListPrint.rdlc";

        //    Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
        //    rsInvoice.Name = "PickingList";
        //    rsInvoice.Value = dtoPrintout.PickingListPrintouts;
        //    lr.DataSources.Add(rsInvoice);

        //    Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
        //    rsInvoiceDetail.Name = "PickingListDetail";
        //    rsInvoiceDetail.Value = dtoPrintout.PickingListDetailPrintouts;
        //    lr.DataSources.Add(rsInvoiceDetail);

        //    string printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");

        //    if (notification.Type == Library.DTO.NotificationType.Error)
        //    {
        //        return InternalServerError(new Exception(notification.Message));
        //    }
        //    return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        //}

        [HttpPost]
        [Route("printpickinglist")]
        public IHttpActionResult GetReportData(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            string dataFileName = bll.GetPickingListPrintoutData(id,ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("printCMR")]
        public IHttpActionResult PrintPickingList(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //GET DATA
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            DTO.WarehousePickingListMng.CMRContainer dtoPrintout = bll.GetCMRPrintout(id, ControllerContext.GetAuthUserId(), out notification);

            //CREATE PRINTOUT
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + "CMR.rdlc";

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoice.Name = "CMR";
            rsInvoice.Value = dtoPrintout.CMRs;
            lr.DataSources.Add(rsInvoice);

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoiceDetail.Name = "CMRDetail";
            rsInvoiceDetail.Value = dtoPrintout.CMRDetails;
            lr.DataSources.Add(rsInvoiceDetail);

            string printoutFileName = PrintoutHelper.BuildPrintoutFileCMR(lr, "PDF");

            
            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }

        [HttpPost]
        [Route("printNewPickingList")]
        public IHttpActionResult PrintNewPickingList(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //GET DATA
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            DTO.WarehousePickingListMng.PickingListContainerPrintout dtoPrintout = bll.GetNewPickingListPrintData(id, ControllerContext.GetAuthUserId(), out notification);

            //CREATE PRINTOUT
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + "PickingListPrint.rdlc";

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoice.Name = "PickingList";
            rsInvoice.Value = dtoPrintout.PickingListPrintouts;
            lr.DataSources.Add(rsInvoice);

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoiceDetail.Name = "PickingListDetail";
            rsInvoiceDetail.Value = dtoPrintout.PickingListDetailPrintouts;
            lr.DataSources.Add(rsInvoiceDetail);

            string printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");


            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }

        [HttpPost]
        [Route("exportXML")]
        public IHttpActionResult ExportXML(int id)
        {
            Library.DTO.Notification notification;
            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            string xmlFile = bll.ExportXML(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<string>() { Data = xmlFile, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelpickinglist")]
        public IHttpActionResult GetExportExcelPickingList()
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            string dataFileName = bll.GetExportExcelPickingList(ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("deletepickinglistproductdetail")]
        public IHttpActionResult DeletePickingListProductDetail(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            bll.DeletePickingListProductDetail(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("deletepickinglistsparepartdetail")]
        public IHttpActionResult DeletePickingListSparepartDetail(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            bll.DeletePickingListSparepartDetail(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("deletepickinglistareadetail")]
        public IHttpActionResult DeletePickingListAreaDetail(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehousePickingListMng bll = new BLL.WarehousePickingListMng();
            bll.DeletePickingListAreaDetail(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

    }
}
