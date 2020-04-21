using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.ComponentModel.DataAnnotations;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/saleordermng")]
    public class SaleOrderMngController : ApiController
    {
        private string moduleCode = "SaleOrderMng";
        private Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.SaleOrderMng.Executor, Module.SaleOrderMng");
        Module.Framework.BLL fwBll = new Module.Framework.BLL();
        
        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            int totalRows = 0;

            object data = executor.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int offerID, string orderType = null)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            input["offerID"] = offerID;
            input["orderType"] = orderType;
            object SaleOrder = executor.CustomFunction(ControllerContext.GetAuthUserId(),"getsaleorder",input, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = SaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, object dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                // create new case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //// validation            
            //if (!Helper.CommonHelper.ValidateDTO<object>(dtoItem, out notification))
            //{
            //    return Ok(new Library.DTO.ReturnData<DTO.SaleOrder>() { Data = dtoItem, Message = notification });
            //}

            //// continue processing
            //BLL.SaleOrderMng bll = new BLL.SaleOrderMng();

            ////validate quantity in stock
            //if (!bll.ValidateStockQuantity(id, dtoItem, ControllerContext.GetAuthUserId(), out notification))
            //{
            //    return Ok(new Library.DTO.ReturnData<DTO.SaleOrder>() { Data = dtoItem, Message = notification });
            //}

            executor.UpdateData(ControllerContext.GetAuthUserId(), id, ref dtoItem, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            Library.DTO.Notification notification;
            executor.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-line")]
        public IHttpActionResult GetOfferLine(int offerId, string orderType)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["offerID"] = offerId;
            input["orderType"] = orderType;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getofferline" ,input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-offer-line-sparepart")]
        public IHttpActionResult GetOfferLineSparepart(int offerId, string orderType)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["offerID"] = offerId;
            input["orderType"] = orderType;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getofferlinesparepart", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("confirm")]
        public IHttpActionResult Confirm(int id, object dtoItem, bool withoutSigned)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            input["dtoItem"] = dtoItem;
            input["withoutSigned"] = withoutSigned;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "confirm", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("revise")]
        public IHttpActionResult Revise(int id, object dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            input["dtoItem"] = dtoItem;
            executor.CustomFunction(ControllerContext.GetAuthUserId(), "revise", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("printpi")]
        public IHttpActionResult PrintPI(int id, string reportName)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            //GET DATA
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = id;
            input["reportName"] = reportName;
            object fileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "printpi", input , out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = fileName, Message = notification });
        }

        [HttpPost]
        [Route("uploadSignedPIFile")]
        public IHttpActionResult UploadSignedPIFile(int saleOrderID, string newFile, string oldPointer)
        {

            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = saleOrderID;
            input["newFile"] = newFile;
            input["oldPointer"] = oldPointer;
            input["tempFolder"] = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            object dtoSaleOrder = executor.CustomFunction(ControllerContext.GetAuthUserId(), "upload-signed-pi-file", input, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoSaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("uploadClientPOFile")]
        public IHttpActionResult UploadClientPOFile(int saleOrderID, string newFile, string oldPointer)
        {
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = saleOrderID;
            input["newFile"] = newFile;
            input["oldPointer"] = oldPointer;
            input["tempFolder"] = Helper.AuthHelper.GetCurrentUserFolder(ControllerContext);
            object dtoSaleOrder = executor.CustomFunction(ControllerContext.GetAuthUserId(), "up-load-client-po-file", input, out notification);

            return Ok(new Library.DTO.ReturnData<object>() { Data = dtoSaleOrder, Message = notification });
        }

        [HttpPost]
        [Route("getloadingplan2")]
        public IHttpActionResult GetLoadingPlan2(int saleOrderID)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["ID"] = saleOrderID;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "getloadingplan2", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("returngoods")]
        public IHttpActionResult CreateReturnData(object dtoReturns)
        {
            BLL.SaleOrderMng bll = new BLL.SaleOrderMng();
            Library.DTO.Notification notification;
            System.Collections.Hashtable input = new System.Collections.Hashtable();
            input["dtoReturns"] = dtoReturns;
            object data = executor.CustomFunction(ControllerContext.GetAuthUserId(), "returngoods", input, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }
    }
}