﻿using Library.DTO;
using System;
using System.Collections;
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
    [RoutePrefix("api/labelingpackaging")]
    public class LabelingPackagingController : ApiController
    {
        private string moduleCode = "LabelingPackaging";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.LabelingPackaging.Executor, Module.LabelingPackaging");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            // authentication
            BLL.Framework fwBll = new BLL.Framework();
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

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        //
        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getexceldata")]
        public IHttpActionResult GetExcelData(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["id"] = id;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexceldata", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        //
        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(DTO.Search searchInput)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            Library.DTO.Notification notification;

            inputs["SortedBy"] = searchInput.SortedBy;
            inputs["SortedDirection"] = searchInput.SortedDirection;
            inputs["Season"] = searchInput.Filters["Season"];
            inputs["SaleID"] = searchInput.Filters["SaleID"];
            inputs["ProformaInvoiceNo"] = searchInput.Filters["ProformaInvoiceNo"];
            inputs["ClientUD"] = searchInput.Filters["ClientUD"];
            inputs["FactoryUD"] = searchInput.Filters["FactoryUD"];
            inputs["LPStatusID"] = searchInput.Filters["LPStatusID"];

            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpGet]
        [Route("clearexistingfile")]
        public IHttpActionResult ClearExistingFile()
        {
            Library.DTO.Notification notification;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "clearexistingfile", null, out notification);
            return Ok("OK");
        }

        [HttpPost]
        [Route("sendnotificationnl2vn")]
        public IHttpActionResult SendNotificationNL2VN(int? factoryID)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable() { ["FactoryID"] = factoryID };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "sendnotificationnl2vn", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("autofile")]
        public IHttpActionResult AutoFile(int id, int? chkPosition, object dtoItems)
        {
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable dataItems = new Hashtable();
            dataItems["data"] = dtoItems;
            dataItems["chkPosition"] = chkPosition;
            dataItems["ID"] = id;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "AutoFile", dataItems, out Notification notification);
            return Ok(new ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("autoupdate")]
        public IHttpActionResult AutoUpdate(int id, object dtoItem)
        {
            // authentication
            BLL.Framework fwBll = new BLL.Framework();
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

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            Hashtable dataItems = new Hashtable();
            dataItems["data"] = dtoItem;
            dataItems["LPRid"] = id;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "autoupdate", dataItems, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }
    }
}
