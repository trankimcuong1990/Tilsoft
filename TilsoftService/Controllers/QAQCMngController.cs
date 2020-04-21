using DTO.QAQCMng;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using TilsoftService.Helper;
using System.IO;
using System.Web;

namespace TilsoftService.Controllers
{
    //[Authorize]
    //[TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/qaqcmng")]
    public class QAQCMngController : ApiController
    {
        private string moduleCode = "QAQCMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.QAQCMng.Executor, Module.QAQCMng");
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
        [Route("getqaqcbyuser")]
        public IHttpActionResult GetQAQCByUserID(string strUserID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            object data = null;
            if (UserID.HasValue)
            {
                // authentication
                if (!fwBll.CanPerformAction(UserID.Value, moduleCode, Library.DTO.ModuleAction.CanRead))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }

                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                data = executor.CustomFunction(UserID.Value, "api-get-qaqc-by-userid", inputs, out notification);
                
            }
            return Ok(data);
        }
        [HttpPost]
        [Route("getcategory")]
        public IHttpActionResult GetCategory(string strUserID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            object data = null;
            if (UserID.HasValue)
            {
                // authentication
                if (!fwBll.CanPerformAction(UserID.Value, moduleCode, Library.DTO.ModuleAction.CanRead))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }

                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                data = executor.CustomFunction(UserID.Value, "api-get-category", inputs, out notification);

            }
            return Ok(data);
        }

        [HttpPost]
        [Route("updatereport")]
        public IHttpActionResult UpdateReport(object report)
        {
         
            ReportMobileInputDTO temp = ((Newtonsoft.Json.Linq.JObject)report).ToObject<ReportMobileInputDTO>();
            string strUserID = temp.StrUserID;
            Library.DTO.Notification notification = new Library.DTO.Notification();
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            object data = null;
            if (UserID.HasValue)
            {
                // authentication
                if (!fwBll.CanPerformAction(UserID.Value, moduleCode, Library.DTO.ModuleAction.CanUpdate))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }

                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                inputs["report"] = temp;
                data = executor.CustomFunction(UserID.Value, "api-update-report", inputs, out notification);

            }
            return Ok(data);
        }

        [HttpPost]
        [Route("get-status")]
        public IHttpActionResult GetStatus(string strUserID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            if (UserID == null || UserID == 0)
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = null;
            if (UserID.HasValue)
            {
                // authentication
                if (!fwBll.CanPerformAction(UserID.Value, moduleCode, Library.DTO.ModuleAction.CanRead))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }

                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                data = executor.CustomFunction(UserID.Value, "api-get-status", inputs, out notification);

            }
            return Ok(data);
        }

        [HttpPost]
        [Route("make-process")]
        public IHttpActionResult MakeProcess(string strUserID, string managementStringID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            if (UserID == null || UserID == 0)
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = null;
            if (UserID.HasValue)
            {
                // authentication
                if (!fwBll.CanPerformAction(UserID.Value, moduleCode, Library.DTO.ModuleAction.CanRead))
                {
                    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
                }

                System.Collections.Hashtable inputs = new System.Collections.Hashtable();
                inputs["managementStringID"] = managementStringID;
                data = executor.CustomFunction(UserID.Value, "api-make-process", inputs, out notification);
            }
            return Ok(data);
        }


        [HttpPost]
        [Route("search-data-report")]
        public IHttpActionResult GetsEcommercial(DTO.Search searchInput)
        {
            Hashtable input = searchInput.Filters;
            input["pageSize"] = searchInput.PageSize;
            input["pageIndex"] = searchInput.PageIndex;
            input["sortedBy"] = searchInput.SortedBy;
            input["sortedDirection"] = searchInput.SortedDirection;

            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "search-data-report", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, });
        }

        [HttpPost]
        [Route("get-data-report")]
        public IHttpActionResult SetStatus(int reportQAQCID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["reportQAQCID"] = reportQAQCID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-data-report", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("set-status-report")]
        public IHttpActionResult SetStatusReport(int reportQAQCID, int statusID, string comment)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["reportQAQCID"] = reportQAQCID;
            inputs["statusID"] = statusID;
            inputs["comment"] = comment;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "set-status-report", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification});
        }

        [HttpPost]
        [Route("set-tracking-factory-order")]
        public IHttpActionResult SetTrackingFacoryOrDer(string strUserID, object dtoItem)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // authentication
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            if (UserID == null && UserID <= 0) {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable param = new Hashtable();
            param["data"] = dtoItem;
            object data = executor.CustomFunction(UserID.Value, "set-tracking-factory-order", param, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("get-loginspector")]
        public IHttpActionResult GetLogInspector(int qaqcid)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["QAQCID"] = qaqcid;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-loginspector", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult LoginUser(string strUserID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            int? UserID = fwBll.GetUserIDFromSecreteKey(strUserID, out notification);
            if (UserID == null || UserID == 0)
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            object data = data = executor.CustomFunction(UserID.Value, "login", null, out notification);
            return Ok(data);
        }

        [HttpPost]
        [Route("upload-file")]
        public IHttpActionResult UploadFile()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
           
            string result = "";

            if (ModelState.IsValid)
            {
                try
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/"));
                    }
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Media/Temp/" + "/android/");

                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var filex =  HttpContext.Current.Request.Files[i];
                        if (filex != null)
                        {
                            string tempFile = Path.Combine(uploadPath, Path.GetFileName(filex.FileName));
                            filex.SaveAs(tempFile);
                        }
                    }
                    result = "OK";
                }
                catch (IOException ex)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = Library.Helper.GetInnerException(ex).Message;
                    result = Library.Helper.GetInnerException(ex).Message;
                }
            }
            return Ok(result);
        }
    }
}