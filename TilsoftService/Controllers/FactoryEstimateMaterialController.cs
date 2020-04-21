using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/factoryEstimateMaterial")]
    public class FactoryEstimateMaterialController : ApiController
    {
        private string moduleCode = "FactoryEstimateMaterialMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.FactoryEstimateMaterial.Executor, Module.FactoryEstimateMaterial");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();

        [HttpPost]
        [Route("esimatematerial")]
        public IHttpActionResult EstimateMaterial(string factoryOrderIds)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["factoryOrderIds"] = factoryOrderIds;

            //get data
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "EstimateMaterial", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("importmaterial")]
        public IHttpActionResult ImportMaterial(object dtoItem)
        {
            //get input
            Hashtable filters = new Hashtable();
            filters["dtoItem"] = dtoItem;

            //get data
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ImportMaterial", filters, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getSupportData")]
        public IHttpActionResult GetSupportData()
        {
            //get data
            Library.DTO.Notification notification;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "GetSupportData", null, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
