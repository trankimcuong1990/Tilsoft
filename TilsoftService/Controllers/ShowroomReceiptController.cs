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
    [RoutePrefix("api/showroomreceipt")]
    public class ShowroomReceiptController : ApiController
    {
        private string moduleCode = "ShowroomReceiptMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ShowroomReceiptMng bll = new BLL.ShowroomReceiptMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ShowroomReceiptMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShowroomReceiptMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ShowroomReceiptMng bll = new BLL.ShowroomReceiptMng();
            Library.DTO.Notification notification;
            DTO.ShowroomReceiptMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShowroomReceiptMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem)
        {
            Library.DTO.Notification notification;
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.ShowroomReceiptMng.ShowroomReceipt>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.ShowroomReceiptMng.ShowroomReceipt>() { Data = dtoItem, Message = notification });
            }
            // save data
            BLL.ShowroomReceiptMng bll = new BLL.ShowroomReceiptMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShowroomReceiptMng.ShowroomReceipt>() { Data = dtoItem, Message = notification });
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
            BLL.ShowroomReceiptMng bll = new BLL.ShowroomReceiptMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }
	}
}