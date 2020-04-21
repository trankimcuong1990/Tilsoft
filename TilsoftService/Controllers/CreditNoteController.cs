using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.ComponentModel.DataAnnotations;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/creditnote")]
    public class CreditNoteController : ApiController
    {
        private string moduleCode = "CreditNoteMng";

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int eCommercialInvoiceID)
        {
            //authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.CreditNoteMng bll = new BLL.CreditNoteMng();
            Library.DTO.Notification notification;
            DTO.CreditNoteMng.CreditNote data = bll.GetEditData(id, eCommercialInvoiceID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.CreditNoteMng.CreditNote>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.CreditNoteMng.CreditNote dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.CreditNoteMng.CreditNote>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.CreditNoteMng.CreditNote>() { Data = dtoItem, Message = notification });
            }
            // save data
            BLL.CreditNoteMng bll = new BLL.CreditNoteMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.CreditNoteMng.CreditNote>() { Data = dtoItem, Message = notification });
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

            BLL.CreditNoteMng bll = new BLL.CreditNoteMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("printcreditnote")]
        public IHttpActionResult PrintCreditNote(int id, string fileTypeID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            //GET DATA
            BLL.CreditNoteMng bll = new BLL.CreditNoteMng();
            DTO.ECommercialInvoiceMng.InvoiceContainerPrintout dtoPrintout = bll.GetCreditNotePrintout(id, ControllerContext.GetAuthUserId(), out notification);

            //CREATE PRINTOUT
            string printoutFileName = string.Empty;
            if (dtoPrintout != null && dtoPrintout.Invoices != null && dtoPrintout.InvoiceDetails != null)
            {
                Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                lr.ReportPath = FrameworkSetting.Setting.AbsoluteReportFolder + "CreditNotePrint.rdlc";

                Microsoft.Reporting.WebForms.ReportDataSource rsInvoice = new Microsoft.Reporting.WebForms.ReportDataSource();
                rsInvoice.Name = "Invoice";
                rsInvoice.Value = dtoPrintout.Invoices;
                lr.DataSources.Add(rsInvoice);

                Microsoft.Reporting.WebForms.ReportDataSource rsInvoiceDetail = new Microsoft.Reporting.WebForms.ReportDataSource();
                rsInvoiceDetail.Name = "InvoiceDetail";
                rsInvoiceDetail.Value = dtoPrintout.InvoiceDetails;
                lr.DataSources.Add(rsInvoiceDetail);

                printoutFileName = PrintoutHelper.BuildPrintoutFile(lr, fileTypeID);
            }
            return Ok(new Library.DTO.ReturnData<string>() { Data = printoutFileName, Message = notification });
        }

        [HttpPost]
        [Route("confirm")]
        public IHttpActionResult Confirm(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.CreditNoteMng bll = new BLL.CreditNoteMng();
            Library.DTO.Notification notification;
            bll.Confirm(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }
    }
}
