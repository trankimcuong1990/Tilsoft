using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;
namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/reportshowroomoverview")]
    public class ReportShowroomOverviewController : ApiController
    {
        private string moduleCode = "ReportShowroomOverviewMng";

        [HttpPost]
        [Route("getshowroomoverview")]
        public IHttpActionResult GetShowroomOverview(Hashtable filters)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportShowroomOverview bll = new BLL.ReportShowroomOverview();
            List<DTO.ReportShowroomOverview.ShowroomArea> data = bll.GetReportShowroomOverview(filters, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ReportShowroomOverview.ShowroomArea>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("printhangtag")]
        public IHttpActionResult PringHangTag(string keyIDs)
        {
            Library.DTO.Notification notification;
            BLL.ReportShowroomOverview bll = new BLL.ReportShowroomOverview();
            string data = bll.PrinHangTag(keyIDs, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("printshowroomoverview")]
        public IHttpActionResult PrintShowroomOverview()
        {
            Library.DTO.Notification notification;
            BLL.ReportShowroomOverview bll = new BLL.ReportShowroomOverview();
            string data = bll.PrintShowroomOverview(out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = data, Message = notification });
        }
    }
}
