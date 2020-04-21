using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [RoutePrefix("api/usagetrackingrpt")]
    public class UsageTrackingRptController : ApiController
    {
        private string moduleCode = "UsageTrackingRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.UsageTrackingRpt.Executor, Module.UsageTrackingRpt");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilterData()
        {
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsearchfilter", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getconclusion")]
        public IHttpActionResult GetConclusion(string f, string t, string e, string c, string m, string l)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = c;
            param["module"] = m;
            param["location"] = l;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getconclusion", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getbrowser")]
        public IHttpActionResult GetBrowser(string f, string t, string e, string c, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = c;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getbrowser", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getcompany")]
        public IHttpActionResult GetCompany(string f, string t, string e, string c, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = c;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getcompany", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getip")]
        public IHttpActionResult GetIP(string f, string t, string e, string c, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = c;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getip", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getmodule")]
        public IHttpActionResult GetModule(string f, string t, string e, string c, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = c;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getmodule", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getuser")]
        public IHttpActionResult GetUser(string f, string t, string e, string c, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = c;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getuser", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getuserdetail")]
        public IHttpActionResult GetUserDetail(int id, string f, string t, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["fromdate"] = f;
            param["todate"] = t;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getuserdetail", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getcompanydetail")]
        public IHttpActionResult GetCompanyDetail(int id, string f, string t, string m)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["id"] = id;
            param["fromdate"] = f;
            param["todate"] = t;
            param["module"] = m;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getcompanydetail", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getmoduledetail")]
        public IHttpActionResult GetModuleDetail(string c, string f, string t, string e, string co)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            Hashtable param = new Hashtable();
            param["controllername"] = c;
            param["fromdate"] = f;
            param["todate"] = t;
            param["employee"] = e;
            param["company"] = co;
            var dtoItem = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getmoduledetail", param, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("preparecachedata")]
        public IHttpActionResult PrepareCacheData()
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification;
            object result = executor.CustomFunction(ControllerContext.GetAuthUserId(), "preparecachedata", new Hashtable(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = result, Message = notification });
        }
    }
}
