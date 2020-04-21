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
    [RoutePrefix("api/dashboardmng")]
    public class DashboardMngController : ApiController
    {
        private string moduleCode = "DashboardMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.DashboardMng.Executor, Module.DashboardMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(string season)
        {
            // authentication - check if user is in admin group
            if(fwBll.GetUserGroup(ControllerContext.GetAuthUserId()) != 1)
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            Library.DTO.Notification notification;
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getreportdata", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getDataForUserDashboard")]
        public IHttpActionResult GetDataForUserDashboard(string season)
        {
            // authentication - check if user is in admin group
            //if (fwBll.GetUserGroup(ControllerContext.GetAuthUserId()) != 1)
            //{
            //    return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            //}

            ///executor.identifier = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);

            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["season"] = season
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getDataForUserDashboard", inputs, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getproductiondata")]
        public IHttpActionResult GetDataForProductionOverview(string season)
        {
            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["season"] = season
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getProductionData", inputs, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getpendingpricedata")]
        public IHttpActionResult GetDataForPendingPrice()
        {           
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpendingpricedata", new System.Collections.Hashtable(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getDataForProductionOverviewDetail")]
        public IHttpActionResult GetDataForProductionOverviewDetail(int factoryOrderID)
        {
            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["factoryOrderID"] = factoryOrderID
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getDataForProductionOverviewDetail", inputs, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getweeklyproductiondata")]
        public IHttpActionResult GetWeeklyProductionData(string season, int factoryId)
        {
            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["season"] = season,
                ["factoryId"] = factoryId
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getweeklyproductiondata", inputs, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getFinanceOverviewByFactoryID")]
        public IHttpActionResult GetFinanceOverviewByFactory(string season, int factoryID)
        {
            System.Collections.Hashtable inputs = new System.Collections.Hashtable
            {
                ["season"] = season,
                ["factoryID"] = factoryID
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getFinanceOverviewByFactory", inputs, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoffernotapprovedyet")]
        public IHttpActionResult GetOfferNotApprovedYet(string season)
        {
            System.Collections.Hashtable param = new System.Collections.Hashtable();
            param["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getoffernotapprovedyet", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getoffernotapprovedyetuserdashboard")]
        public IHttpActionResult GetOfferNotApprovedYetUserDashbroad()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getoffernotapprovedyetuserdashboard", new System.Collections.Hashtable(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-delta-compare-offer-with-pi-last-year")]
        public IHttpActionResult GetDeltaCompareOfferWithPILastYear()
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-delta-compare-offer-with-pi-last-year", new System.Collections.Hashtable(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-delta-by-client-compare")]
        public IHttpActionResult GetDeltaByClientCompare(string season)
        {
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-delta-by-client-compare", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-pending-offer-item-price")]
        public IHttpActionResult GetPendingOfferItemPrice(string season)
        {
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-pending-offer-item-price", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-item-delta-need-attention")]
        public IHttpActionResult GetItemDeltaNeedAttention(DTO.Search searchInput)
        {
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-item-delta-need-attention", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<Object>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("gethtmlreport")]
        public IHttpActionResult GetHTMLReportData(string season, int factoryID)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["season"] = season;
            inputs["factoryID"] = factoryID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "gethtmlreport", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
        [HttpPost]
        [Route("dashboarddetal")]
        public IHttpActionResult GetDashboardDetal(string season)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            System.Collections.Hashtable inputs = new System.Collections.Hashtable();
            inputs["season"] = season;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "dashboarddetal", inputs, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
