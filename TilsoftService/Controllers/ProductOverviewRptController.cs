using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/productoverviewrpt")]
    public class ProductOverviewRptController : ApiController
    {
        private string moduleCode = "ProductOverviewRpt";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductOverviewRpt.Executor, Module.ProductOverviewRpt");
        private Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("getoverviewdata")]
        public IHttpActionResult GetOverviewData(int id, int? offerSeasonDetailID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["offerSeasonDetailID"] = offerSeasonDetailID
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getoverviewdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getitemtocomparedata")]
        public IHttpActionResult GetItemToCompareData(int id, int? offerSeasonDetailID)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id,
                ["offerSeasonDetailID"] = offerSeasonDetailID
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getitemtocomparedata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getcomparableitemdata")]
        public IHttpActionResult GetComparableItemData(Hashtable filters)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["filters"] = filters
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getcomparableitemdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getbestcomparableitemdata")]
        public IHttpActionResult GetBestComparableItemData(Hashtable filters)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["filters"] = filters
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getbestcomparableitemdata", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getpriceofferhistory")]
        public IHttpActionResult GetPriceOfferHistory(int id)
        {
            // authentication
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Hashtable param = new Hashtable
            {
                ["id"] = id
            };
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getpriceofferhistory", param, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
