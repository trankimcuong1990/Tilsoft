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
    [RoutePrefix("api/ReportPlcProducts")]
    public class ReportPlcProductsController : ApiController
    {
        private string moduleCode = "ReportPlcProducts";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReport(int factoryID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportPlcProducts bll = new BLL.ReportPlcProducts();
            string reportFileName = bll.GetReportPlcProducts(factoryID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }
        //[HttpPost]
        //[Route("get")]
        //public IHttpActionResult Get(int id)
        //{
        //    // authentication
        //    Module.Framework.BLL fwBll = new Module.Framework.BLL();
        //    if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
        //    {
        //        return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
        //    }
        //    BLL.ReportPlcProducts bll = new BLL.ReportPlcProducts();
        //    Library.DTO.Notification notification;
        //    DTO.ReportPlcProducts.SupportDataContainer data = bll.GetReportPlcProducts (id, ControllerContext.GetAuthUserId(), out notification);
        //    return Ok(new Library.DTO.ReturnData<DTO.ReportPlcProduct.>() { Data = data, Message = notification });
        //}
        [HttpPost]
        [Route("getfilters")]
        public IHttpActionResult GetFilters()
        {
            BLL.ReportPlcProducts bll = new BLL.ReportPlcProducts();
            Library.DTO.Notification notification;
            DTO.ReportPlcProducts.SupportDataContainer result = bll.GetFilters(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportPlcProducts.SupportDataContainer>() { Data = result, Message = notification, TotalRows = 0 });
        }
        [HttpPost]
        [Route("print")]
        public IHttpActionResult Print(int plcID)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ReportPlcProducts bll = new BLL.ReportPlcProducts();
            string reportFileName = bll.GetReportPlcProducts (plcID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }
    }
}
