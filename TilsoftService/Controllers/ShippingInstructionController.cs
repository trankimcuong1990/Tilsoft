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
    [RoutePrefix("api/shippinginstruction")]
    public class ShippingInstructionController : ApiController
    {
        private string moduleCode = "ShippingInstructionMng";

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

            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ShippingInstructionMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
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

            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            Library.DTO.Notification notification;
            DTO.ShippingInstructionMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.EditFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ShippingInstructionMng.ShippingInstruction dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.ShippingInstructionMng.ShippingInstruction>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.ShippingInstruction>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, DTO.ShippingInstructionMng.ShippingInstruction dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.ShippingInstructionMng.ShippingInstruction>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            bll.Approve(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.ShippingInstruction>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, DTO.ShippingInstructionMng.ShippingInstruction dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.ShippingInstructionMng.ShippingInstruction>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            bll.Reset(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.ShippingInstruction>() { Data = dtoItem, Message = notification });
        }

        //
        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchSupportData()
        {
            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            Library.DTO.Notification notification;
            DTO.ShippingInstructionMng.SearchFilterData data = bll.GetFilterData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("getdefault")]
        public IHttpActionResult GetDefault(int clientId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            Library.DTO.Notification notification;
            DTO.ShippingInstructionMng.ShippingInstruction data = bll.GetDefault(clientId, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.ShippingInstruction>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("getcountrybypod")]
        public IHttpActionResult GetCountryFromPODfunction(int podid)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            Library.DTO.Notification notification;
            DTO.ShippingInstructionMng.PODCountry data = bll.GetCountryFromPODfunction(podid, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShippingInstructionMng.PODCountry>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("exportexcel")]
        public IHttpActionResult ExportExcel(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;
            BLL.ShippingInstructionMng bll = new BLL.ShippingInstructionMng();
            string dataFileName = bll.ExportExcel(searchInput.Filters, out notification);
            //string dataFileName = string.Empty;
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}
