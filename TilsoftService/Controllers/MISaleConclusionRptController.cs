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
    [RoutePrefix("api/misaleconclusion")]
    public class MISaleConclusionRptController : ApiController
    {
        private string moduleCode = "MISaleConclusionRpt";

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReport(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<DTO.MISaleConclusionRpt.ReportData>() { Data = bll.GetReportData(ControllerContext.GetAuthUserId(), season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-pi-by-country")]
        public IHttpActionResult GetPiByCountry(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.ProformaCountry>>() { Data = bll.GetPiByCountry(season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-pi-confirmed-by-country")]
        public IHttpActionResult GetPiConfirmedByCountry(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry>>() { Data = bll.GetPiConfirmedByCountry(season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-ci-confirmed-by-country")]
        public IHttpActionResult GetCiByCountry(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry>>() { Data = bll.GetCiByCountry(season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-delta-by-country")]
        public IHttpActionResult GetDeltaByClient(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.Delta>>() { Data = bll.GetDeltaByClient(season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-expected-by-country")]
        public IHttpActionResult GetExpectedByCountry(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.ExpectedCountry>>() { Data = bll.GetExpectedByCountry(season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-expected-by-client")]
        public IHttpActionResult GetExpectedByClient(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.ExpectedByClient>>() { Data = bll.GetExpectedByClient(season, saleId, out notification), Message = notification });
        }

        [HttpPost]
        [Route("get-range-expected")]
        public IHttpActionResult GetRangeExpected(string season, int? saleId)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.MISaleConclusionRpt bll = new BLL.MISaleConclusionRpt();
            Library.DTO.Notification notification = new Library.DTO.Notification();
            return Ok(new Library.DTO.ReturnData<List<DTO.MISaleConclusionRpt.RangeExpected>>() { Data = bll.GetRangeExpected(season, saleId, out notification), Message = notification });
        }
    }
}
