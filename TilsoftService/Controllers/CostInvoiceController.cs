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
    [RoutePrefix("api/CostInvoice")]
    public class CostInvoiceController : ApiController
    {

        private string moduleCode = "CostInvoiceMng";

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

            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.CostInvoiceMng.CostInvoiceSearchResult> searchResult = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.CostInvoiceMng.CostInvoiceSearchResult>>() { Data = searchResult, Message = notification, TotalRows = totalRows });
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

            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            Library.DTO.Notification notification;
            DTO.CostInvoiceMng.DataContainer CostInvoice = bll.GetDataContainer(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.CostInvoiceMng.DataContainer>() { Data = CostInvoice, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.CostInvoiceMng.CostInvoice dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.CostInvoiceMng.CostInvoice>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.CostInvoiceMng.CostInvoice>() { Data = dtoItem, Message = notification });
            }

            // continue processing
             BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.CostInvoiceMng.CostInvoice>() { Data = dtoItem, Message = notification });
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

            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("initinfosdetails")]
        public IHttpActionResult GetInitInfoDetails(int eCommercialInvoiceID,int bookingID, int clientID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.CostInvoiceMng.InitInfoDetail> initInfoDetails = bll.GetInitInfoDetails(eCommercialInvoiceID, bookingID, clientID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.CostInvoiceMng.InitInfoDetail>>() { Data = initInfoDetails, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("initinfos")]
        public IHttpActionResult GetInitInfos()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.CostInvoiceMng.InitInfo> initInfos = bll.GetInitInfos(ControllerContext.GetAuthUserId(), out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.CostInvoiceMng.InitInfo>>() { Data = initInfos, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("printinvoice")]
        public IHttpActionResult PrintInvoice(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //GET DATA
            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            DTO.CostInvoiceMng.InvoiceContainerPrintout dtoPrintout = bll.GetPrintoutData(id, ControllerContext.GetAuthUserId(), out notification);

            //CREATE PRINTOUT
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + "CostInvoicePrint.rdlc";

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoice.Name = "Invoice";
            rsInvoice.Value = dtoPrintout.Invoices;
            lr.DataSources.Add(rsInvoice);

            Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
            rsInvoiceDetail.Name = "InvoiceDetail";
            rsInvoiceDetail.Value = dtoPrintout.InvoiceDetails;
            lr.DataSources.Add(rsInvoiceDetail);

            string printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, "PDF");
            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }


        [HttpPost]
        [Route("quicksearchcontainer")]
        public IHttpActionResult GetInitInfoDetails(int eCommercialInvoiceID,int bookingID, int clientID,string containerNo)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.CostInvoiceMng bll = new BLL.CostInvoiceMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.CostInvoiceMng.InitInfoDetail> initInfoDetails = bll.QuickSearchContainer(eCommercialInvoiceID, bookingID, clientID, containerNo, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.CostInvoiceMng.InitInfoDetail>>() { Data = initInfoDetails, Message = notification, TotalRows = totalRows });
        }
    }
}
