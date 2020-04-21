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
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {
        private string moduleCode = "BookingMng";

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

            BLL.BookingMng bll = new BLL.BookingMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.BookingMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int cID, int sID, string se)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.BookingMng bll = new BLL.BookingMng();
            Library.DTO.Notification notification;
            if (id > 0)
            {
                cID = sID = 0;
                se = string.Empty;
            }

            DTO.BookingMng.EditFormData data = bll.GetData(id, cID, sID, se, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.BookingMng.Booking dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.BookingMng.Booking>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.BookingMng.Booking>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.BookingMng bll = new BLL.BookingMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.Booking>() { Data = dtoItem, Message = notification });
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

            BLL.BookingMng bll = new BLL.BookingMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, DTO.BookingMng.Booking dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.BookingMng.Booking>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.BookingMng bll = new BLL.BookingMng();
            bll.Approve(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.Booking>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, DTO.BookingMng.Booking dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.BookingMng.Booking>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.BookingMng bll = new BLL.BookingMng();
            bll.Reset(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.Booking>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilter()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.BookingMng bll = new BLL.BookingMng();
            Library.DTO.Notification notification;
            DTO.BookingMng.SearchFilterData data = bll.GetSearchFilter(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.SearchFilterData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.BookingMng bll = new BLL.BookingMng();
            Library.DTO.Notification notification;
            DTO.BookingMng.InitFormData data = bll.GetInitData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BookingMng.InitFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(int bookingID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.BookingMng bll = new BLL.BookingMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            string reportFileName = bll.GetReportData(bookingID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }


        [HttpPost]
        [Route("confirmETA")]
        public IHttpActionResult ConfirmETA(int bookingID,string ETA, int ETAType)
        {
            Library.DTO.Notification notification;
            string concurrencyFlag_String;
            BLL.BookingMng bll = new BLL.BookingMng();
            bll.ConfirmETA(bookingID, ControllerContext.GetAuthUserId(), ETA, ETAType,out concurrencyFlag_String, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = concurrencyFlag_String, Message = notification });
        }
    }
}
