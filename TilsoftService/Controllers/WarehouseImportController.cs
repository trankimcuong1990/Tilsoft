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
    [RoutePrefix("api/warehouseimport")]
    public class WarehouseImportController : ApiController, IAPIController<DTO.WarehouseImportMng.WarehouseImport>
    {
        public string getModuleCode()
        {
            return "WarehouseImportMng";
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

            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.WarehouseImportMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseImportMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
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

            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            DTO.WarehouseImportMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseImportMng.EditFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.WarehouseImportMng.WarehouseImport dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.WarehouseImportMng.WarehouseImport>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseImportMng.WarehouseImport>() { Data = dtoItem, Message = notification });
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

            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, DTO.WarehouseImportMng.WarehouseImport dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanApprove))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.WarehouseImportMng.WarehouseImport>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            bll.Approve(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.WarehouseImportMng.WarehouseImport>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, DTO.WarehouseImportMng.WarehouseImport dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanReset))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // continue processing
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            bll.Reset(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseImportMng.WarehouseImport>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchonseaproduct")]
        public IHttpActionResult QuickSearchOnSeaProduct(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.WarehouseImportMng.OnSeaProduct> data = bll.QuickSearchOnSeaProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<List<DTO.WarehouseImportMng.OnSeaProduct>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchonseasparepart")]
        public IHttpActionResult QuickSearchOnSeaSparepart(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.WarehouseImportMng.OnSeaSparepart> data = bll.QuickSearchOnSeaSparepart(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<List<DTO.WarehouseImportMng.OnSeaSparepart>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getsearchsupport")]
        public IHttpActionResult GetSearchSupport(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.WarehouseImportMng.SearchFormData data = bll.GetSearchSupport(out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseImportMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchcreditnoteproduct")]
        public IHttpActionResult QuickSearchCreditNoteProduct(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.WarehouseImportMng.CreditNoteProduct> data = bll.QuickSearchCreditNoteProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.WarehouseImportMng.CreditNoteProduct>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchcreditnotesparepart")]
        public IHttpActionResult QuickSearchCreditNoteSparepart(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.WarehouseImportMng.CreditNoteSparepart> data = bll.QuickSearchCreditNoteSparepart(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.WarehouseImportMng.CreditNoteSparepart>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getwarehouseimportprintout")]
        public IHttpActionResult GetImportPrintout(int warehouseImportID, int version)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            string reportFileName = bll.GetImportPrintout(ControllerContext.GetAuthUserId(), warehouseImportID, version, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getExportWexData")]
        public IHttpActionResult GetExportWexData(int warehouseImportID)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            List<DTO.WarehouseImportMng.ExportToWexData> data = bll.GetExportWexData(ControllerContext.GetAuthUserId(), warehouseImportID, out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.WarehouseImportMng.ExportToWexData>>() { Data = data, Message = notification, TotalRows = data.Count() });
        }

        [HttpPost]
        [Route("createWexCSVFile")]
        public IHttpActionResult CreateWexCSVFile(int warehouseImportID)
        {
            Library.DTO.Notification notification;
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), getModuleCode(), Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            string data = bll.CreateWexCSVFile(ControllerContext.GetAuthUserId(), warehouseImportID, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quickSearchSaleOrder")]
        public IHttpActionResult QuickSearchSaleOrder(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.Support.SaleOrder> data = bll.QuickSearchSaleOrder(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.Support.SaleOrder>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quickSearchLoadingPlan")]
        public IHttpActionResult QuickSearchLoadingPlan(DTO.Search searchInput)
        {
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.Support.LoadingPlan> data = bll.QuickSearchLoadingPlan(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.Support.LoadingPlan>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("wexInputData")]
        public IHttpActionResult WexInputData(List<DTO.WarehouseImportMng.WexInputData> wexData)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            BLL.WarehouseImportMng bll = new BLL.WarehouseImportMng();
            int? warehouseImportID = bll.ImportCSVFileToWarehouseImport(wexData, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int?>() { Data = warehouseImportID, Message = notification });
        }

    }
}
