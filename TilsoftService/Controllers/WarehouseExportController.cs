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
    [RoutePrefix("api/warehouseExport")]
    public class WarehouseExportController : ApiController, IAPIController<DTO.WarehouseExportMng.WarehouseExport>
    {
        public string getModuleCode()
        {
            return "WarehouseExportMng";
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

            BLL.WarehouseExportMng bll = new BLL.WarehouseExportMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.WarehouseExportMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseExportMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
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

            BLL.WarehouseExportMng bll = new BLL.WarehouseExportMng();
            Library.DTO.Notification notification;
            DTO.WarehouseExportMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseExportMng.EditFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.WarehouseExportMng.WarehouseExport dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.WarehouseExportMng.WarehouseExport>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.WarehouseExportMng bll = new BLL.WarehouseExportMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseExportMng.WarehouseExport>() { Data = dtoItem, Message = notification });
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

            BLL.WarehouseExportMng bll = new BLL.WarehouseExportMng();
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("changestatus")]
        public IHttpActionResult ChangeStatus(int id, int statusId, DTO.WarehouseExportMng.WarehouseExport dtoItem)
        {
            BLL.WarehouseExportMng bll = new BLL.WarehouseExportMng();
            Library.DTO.Notification notification;
            bll.ChangeStatus(id, statusId, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type != Library.DTO.NotificationType.Success)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.WarehouseExportMng.WarehouseExport>() { Data = dtoItem, Message = notification });
        }


        public IHttpActionResult Approve(int id, DTO.WarehouseExportMng.WarehouseExport dtoItem)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Reset(int id, DTO.WarehouseExportMng.WarehouseExport dtoItem)
        {
            throw new NotImplementedException();
        }
    }
}
