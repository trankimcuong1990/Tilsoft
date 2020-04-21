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
    [RoutePrefix("api/WarehouseTransport")]
    public class WarehouseTransportController : ApiController
    {
        private string moduleCode = "WarehouseTransportMng";

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
            BLL.WarehouseTransportMng bll = new BLL.WarehouseTransportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.WarehouseTransportMng.WarehouseTransportSearch> searchResult = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.WarehouseTransportMng.WarehouseTransportSearch>>() { Data = searchResult, Message = notification, TotalRows = totalRows });
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
            BLL.WarehouseTransportMng bll = new BLL.WarehouseTransportMng();
            Library.DTO.Notification notification;
            DTO.WarehouseTransportMng.WarehouseTransport data = bll.GetEditData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseTransportMng.WarehouseTransport>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.WarehouseTransportMng.WarehouseTransport dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.WarehouseTransportMng.WarehouseTransport>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.WarehouseTransportMng.WarehouseTransport>() { Data = dtoItem, Message = notification });
            }
            // save data
            BLL.WarehouseTransportMng bll = new BLL.WarehouseTransportMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseTransportMng.WarehouseTransport>() { Data = dtoItem, Message = notification });
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
            BLL.WarehouseTransportMng bll = new BLL.WarehouseTransportMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }


        [HttpPost]
        [Route("geteditsupport")]
        public IHttpActionResult GetEditSupport()
        {
            BLL.WarehouseTransportMng bll = new BLL.WarehouseTransportMng();
            DTO.WarehouseTransportMng.EditSupportList data = bll.GetEditSupportData();
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseTransportMng.EditSupportList>() { Data = data });
        }


        [HttpPost]
        [Route("quicksearchproduct")]
        public IHttpActionResult QuickSearchProduct(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.WarehouseTransportMng bll = new BLL.WarehouseTransportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.WarehouseTransportMng.PhysicalStock> searchResult = bll.QuickSearchProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.WarehouseTransportMng.PhysicalStock>>() { Data = searchResult, Message = notification, TotalRows = totalRows });
        }
    }
}
