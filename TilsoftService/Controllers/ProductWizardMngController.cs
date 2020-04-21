using Library.Base;
using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/product-wizard")]
    public class ProductWizardMngController : ApiController
    {
        private IExecutor2 executor = Library.Helper.GetDynamicObject2("Module.ProductWizardMng.Executor, Module.ProductWizardMng");
        [HttpPost]
        [Route("get-product-option")]
        public IHttpActionResult GetProductOption(int? modelID, string season, int? clientID = null)
        {
            System.Collections.Hashtable filters = new System.Collections.Hashtable();
            filters["modelID"] = modelID;
            filters["season"] = season;
            filters["clientID"] = clientID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "get-product-option", filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }        
    }
}
