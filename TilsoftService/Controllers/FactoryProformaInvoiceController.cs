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
    [RoutePrefix("api/factorypi")]
    public class FactoryProformaInvoiceController : ApiController
    {
        private string moduleCode = "FactoryProformaInvoiceMng";
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

            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.FactoryProformaInvoiceMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int factoryId, string season, int factoryOrderId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.FactoryProformaInvoiceMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), factoryId, season, factoryOrderId, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>() { Data = dtoItem, Message = notification });
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

            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.FactoryProformaInvoiceMng.SearchFilterData data = bll.GetFilterData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.FactoryProformaInvoiceMng.InitFormData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.InitFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("quicksearchorderitem")]
        public IHttpActionResult QuickSearchOrderItem(DTO.Search searchInput)
        {
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult> data = bll.QuickSearchItem(searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpGet]
        [Route("quicksearchfactoryorder")]
        public IHttpActionResult QuickSearchFactoryOrder(string season, int? factoryId, string query)
        {
            if (!factoryId.HasValue)
            {
                factoryId = -1;
            }
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult> data = bll.QuickSearchFactoryOrder(ControllerContext.GetAuthUserId(), season, factoryId.Value, query, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.Approve(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.FactoryProformaInvoiceMng bll = new BLL.FactoryProformaInvoiceMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.Reset(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>() { Data = dtoItem, Message = notification });
        }
    }
}
