using Library.DTO;
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
    [Lib.LogFilterAtrribute]
    [RoutePrefix("api/productbreakdowndefaultcategory")]
    public class ProductBreakDownDefaultCategoryMngController : ApiController
    {
        private readonly string moduleCode = "ProductBreakDownDefaultCategoryMng";
        private readonly Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductBreakDownMng.Executor, Module.ProductBreakDownMng");
        private readonly Module.Framework.BLL fwBLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            return Ok();
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilters(DTO.Search inputData)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), inputData.Filters, inputData.PageSize, inputData.PageIndex, inputData.SortedBy, inputData.SortedDirection, out int totalRows, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, TotalRows = totalRows, Message = notification });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["id"] = id,
                ["typeGet"] = 1
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getdata", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object obj)
        {
            if (id == 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            if (id > 0 && !fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["id"] = id,
                ["typeGet"] = 1,
                ["dataView"] = obj
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "updatedata", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteData(int id)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["id"] = id,
                ["typeGet"] = 1
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletedata", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
