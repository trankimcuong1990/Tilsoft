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
    [RoutePrefix("api/eurofarService")]
    public class EurofarServiceController : ApiController
    {
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.EurofarServiceMng.Executor, Module.EurofarServiceMng");
        
        [HttpPost]
        [Route("getWarehousePhysicalStock")]
        public IHttpActionResult GetWarehousePhysicalStock(string userName, string password)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            string errMsg = string.Empty;
            object data = null;
            if (AuthHelper.AuthorizeAPI(userName, password, out errMsg))
            {
                data = executor.CustomFunction(-1, "GetWarehousePhysicalStock", null, out notification);
            }
            else
            {
                notification.Message = errMsg;
            }
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });

        }

        [HttpPost]
        [Route("getWarehouseReservationStockByNLBL")]
        public IHttpActionResult GetWarehouseReservationStockByNLBL(string userName, string password)
        {
            Library.DTO.Notification notification = new Library.DTO.Notification();
            string errMsg = string.Empty;
            object data = null;
            if (AuthHelper.AuthorizeAPI(userName, password, out errMsg))
            {
                data = executor.CustomFunction(-1, "GetWarehouseReservationStockByNLBL", null, out notification);
            }
            else
            {
                notification.Message = errMsg;
            }
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

       
    }
}
