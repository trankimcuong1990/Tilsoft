namespace TilsoftService.Controllers
{
    using Library.Base;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using TilsoftService.Helper;

    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/CashBookReceipt")]
    public class CashBookReceiptController : ApiController
    {
        private string moduleCode = "CashBookReceipt";
        private IExecutor executor = Library.Helper.GetDynamicObject("Module.CashBookReceiptMng.Executor, Module.CashBookReceiptMng");
        private Module.Framework.BLL bLL = new Module.Framework.BLL();

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetInitData(ControllerContext.GetAuthUserId(), out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult GetDataWithFilter(DTO.Search searchInput)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out int totalRows, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetData(int id)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.GetData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateData(int id, object dtoItem)
        {
            if (id == 0 && !bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id > 0 && !bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            object data = executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteData(int id)
        {
            if (!bLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            executor.DeleteData(ControllerContext.GetAuthUserId(), id, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("updatepostcost")]
        public IHttpActionResult UpdatePostCostData(object dtoItem)
        {
            Hashtable input = new Hashtable
            {
                ["data"] = dtoItem
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdatePostCostData", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletepostcost")]
        public IHttpActionResult DeletePostCostData(int id)
        {
            Hashtable input = new Hashtable
            {
                ["id"] = id
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeletePostCostData", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatecostitem")]
        public IHttpActionResult UpdateCostItemData(object dtoItem)
        {
            Hashtable input = new Hashtable
            {
                ["data"] = dtoItem
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateCostItemData", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletecostitem")]
        public IHttpActionResult DeleteCostItemData(int id)
        {
            Hashtable input = new Hashtable
            {
                ["id"] = id
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteCostItemData", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("updatecostitemdetail")]
        public IHttpActionResult UpdateCostItemDetailData(object dtoItem)
        {
            Hashtable input = new Hashtable
            {
                ["data"] = dtoItem
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "UpdateCostItemDetailData", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("deletecostitemdetail")]
        public IHttpActionResult DeleteCostItemDetailData(int id)
        {
            Hashtable input = new Hashtable
            {
                ["id"] = id
            };

            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "DeleteCostItemDetailData", input, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportcashbook")]
        public IHttpActionResult ExportCashBook(DTO.Search searchInput)
        {
            object rptFile = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ExportCashBook", searchInput.Filters, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object> { Data = rptFile, Message = notification });
        }
    }
}
