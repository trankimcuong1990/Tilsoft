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
    [RoutePrefix("api/report_client")]
    public class ReportClientController : ApiController
    {
        private string moduleCode = "ReportClientMng";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportClients()
        {
            Library.DTO.Notification notification;

             
            
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportClientMng  bll = new BLL.ReportClientMng();
            string reportFileName = bll.GetReportClients( ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

      
    }
}
