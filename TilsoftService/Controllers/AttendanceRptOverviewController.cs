﻿using System;
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
    [RoutePrefix("api/attendancerptoverview")]
    public class AttendanceRptOverviewController : ApiController
    {
        private string moduleCode = "AttendanceRptOverview";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.AttendanceRptOverview.Executor, Module.AttendanceRptOverview");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            Object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getexcelreport")]
        public IHttpActionResult GetExcelReportData(string month, string year)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["Month"] = month;
            inputs["Year"] = year;
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getexcelreport", inputs, out notification).ToString();
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }
    }
}