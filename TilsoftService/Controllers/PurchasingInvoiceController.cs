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
    [RoutePrefix("api/PurchasingInvoice")]
    public class PurchasingInvoiceController : ApiController
    {
        private string moduleCode = "PurchasingInvoiceMng";

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
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.PurchasingInvoiceMng.SearchFormData searchFormData = bll.GetSearchData(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.SearchFormData>() { Data = searchFormData, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int invoiceType, int bookingID, int supplierID, int parentID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            DTO.PurchasingInvoiceMng.PurchasingInvoice data = bll.GetEditData(ControllerContext.GetAuthUserId(), id, invoiceType, bookingID, supplierID, parentID, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.PurchasingInvoice>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.PurchasingInvoiceMng.PurchasingInvoice>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.PurchasingInvoice>() { Data = dtoItem, Message = notification });
            }
            // save data
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.PurchasingInvoice>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchsupport")]
        public IHttpActionResult GetSearchSupport()
        {
            Library.DTO.Notification notification;
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            DTO.PurchasingInvoiceMng.SearchSupportList data = bll.GetSearchSupportData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.SearchSupportList>() { Data = data, Message = notification});
        }

        [HttpPost]
        [Route("geteditsupport")]
        public IHttpActionResult GetEditSupport()
        {
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            DTO.PurchasingInvoiceMng.EditSupportList data = bll.GetEditSupportData();
            return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.EditSupportList>() { Data = data });
        }

        [HttpPost]
        [Route("getbookings")]
        public IHttpActionResult GetBookings(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.PurchasingInvoiceMng.Booking> data = bll.GetBookings(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.PurchasingInvoiceMng.Booking>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getloadingplandetail")]
        public IHttpActionResult GetLoadingPlanDetails(int bookingID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanDetail> data = bll.GetLoadingPlanDetails(ControllerContext.GetAuthUserId(), bookingID, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanDetail>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getloadingplansparepartdetail")]
        public IHttpActionResult GetLoadingPlanSparepartDetails(int bookingID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail> data = bll.GetLoadingPlanSparepartDetails(ControllerContext.GetAuthUserId(), bookingID, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.PurchasingInvoiceMng.LoadingPlanSparepartDetail>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("setstatus")]
        public IHttpActionResult SetStatus(int id, bool status, int invoiceType)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            bll.SetStatus(id, ControllerContext.GetAuthUserId(),status, invoiceType, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(int purchasingInvoiceID)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            string reportFileName = bll.GetReportData(ControllerContext.GetAuthUserId(),purchasingInvoiceID, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            DTO.PurchasingInvoiceMng.InitData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.PurchasingInvoiceMng.InitData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportexactonlinesoftware")]
        public IHttpActionResult ExportExactOnlineSoftware(string purchasingInvoiceIds)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            string reportFileName = bll.ExportExactOnlineSoftware(ControllerContext.GetAuthUserId(), purchasingInvoiceIds, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("markasexportedtoexact")]
        public IHttpActionResult MarkAsExportedToExact(List<int> purchasingInvoiceIDs)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            bool result = bll.MarkAsExportedToExact(ControllerContext.GetAuthUserId(), purchasingInvoiceIDs, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("mark-as-not-yet-exported-to-exact")]
        public IHttpActionResult MarkAsNotYetExportedToExact(List<int> purchasingInvoiceIDs)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            bool result = bll.MarkAsNotYetExportedToExact(ControllerContext.GetAuthUserId(), purchasingInvoiceIDs, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("set-confirm-price")]
        public IHttpActionResult SetConfirmPrice(int purchasingInvoiceID, DTO.PurchasingInvoiceMng.PurchasingInvoice dtoItem, bool isConfirmedPrice)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            Library.DTO.Notification notification;
            bll.SetConfirmPrice(ControllerContext.GetAuthUserId(), purchasingInvoiceID, dtoItem, isConfirmedPrice, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = purchasingInvoiceID, Message = notification });
        }

        [HttpPost]
        [Route("print-shipment-purchasing")]
        public IHttpActionResult GetPrintoutData(int purchasingInvoiceID, int optionID)
        {
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.PurchasingInvoiceMng bll = new BLL.PurchasingInvoiceMng();
            object pathFile = bll.GetPrintoutData(ControllerContext.GetAuthUserId(), purchasingInvoiceID, optionID, out Library.DTO.Notification notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = pathFile, Message = notification });
        }
    }
}
