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
    [RoutePrefix("api/userfilemng")]
    public class UserFileMngController : ApiController
    {
        private string moduleCode = "UserFileMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.UserFileMng.Executor, Module.UserFileMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Library.DTO.Notification notification;
            int totalRows = 0;
            Object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("rotatefile")]
        public IHttpActionResult RotateFile(string f, int d)
        {
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["filename"] = f;
            param["direction"] = d;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "rotatefile", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("deletefile")]
        public IHttpActionResult DeleteFile(string f)
        {
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["filename"] = f;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletefile", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }

        [HttpPost]
        [Route("deletefiles")]
        public IHttpActionResult DeleteFiles()
        {
            Library.DTO.Notification notification;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletefiles", new Hashtable(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
