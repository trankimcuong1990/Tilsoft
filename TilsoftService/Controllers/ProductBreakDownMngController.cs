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
    [RoutePrefix("api/productbreakdown")]
    public class ProductBreakDownMngController: ApiController
    {
        private readonly string moduleCode = "ProductBreakDownMng";
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
        public IHttpActionResult GetData(int id, int? modelID, int? sampleProductID)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["id"] = id,
                ["model"] = modelID,
                ["sample"] = sampleProductID,
                ["typeGet"] = 2
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
                ["typeGet"] = 2,
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
                ["typeGet"] = 2
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "deletedata", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult ApproveData(int id)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable filters = new Hashtable()
            {
                ["id"] = id,
                ["typeGet"] = 2
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "approvedata", filters, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchmodel")]
        public IHttpActionResult GetModelQuickSearch(string param)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable() { ["searchQuery"] = param };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchmodel", input, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchsampleproduct")]
        public IHttpActionResult GetSampleProductQuickSearch(string param)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable() { ["searchQuery"] = param };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "quicksearchsampleproduct", input, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("autofillproductbreakdowncategory")]
        public IHttpActionResult GetProductBreakDownCategory(int typeCategoryID)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable() { ["typeCategoryID"] = typeCategoryID };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "autofillproductbreakdowncategory", input, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getsupportclient")]
        public IHttpActionResult GetSupportClient(string param)
        {
            if (!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            Hashtable input = new Hashtable();
            input["searchQuery"] = param;

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getsupportclient", input, out Notification notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }
    }
}
