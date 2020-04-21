using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.ComponentModel.DataAnnotations;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/orderquantitycheckingrpt")]
    public class OrderQuantityCheckingRptController : ApiController
    {
        private string moduleCode = "OrderQuantityCheckingRpt";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.OrderQuantityCheckingRpt bll = new BLL.OrderQuantityCheckingRpt();
            Library.DTO.Notification notification;
            List<DTO.OrderQuantityCheckingRpt.SaleOrder> data = bll.GetOrderQuantityCheckings(out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.OrderQuantityCheckingRpt.SaleOrder>>() { Data = data, Message = notification });
        }
    }
}
