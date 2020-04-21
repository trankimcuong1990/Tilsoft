using System;
using System.Web.Http;
using TilsoftService.Helper;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private string moduleCode = "ProductMng";

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

            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ProductMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ProductMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int modelID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ProductMng.EditFormData data = bll.GetData(id, modelID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ProductMng.EditFormData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ProductMng.Product dtoItem)
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

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.ProductMng.Product>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ProductMng.Product>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, DTO.ProductMng.Product dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.ProductMng.Product>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.Approve(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ProductMng.Product>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, DTO.ProductMng.Product dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // validation
            if (!Helper.CommonHelper.ValidateDTO<DTO.ProductMng.Product>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.Reset(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ProductMng.Product>() { Data = dtoItem, Message = notification });
        }

        //
        // CUSTOM FUNCTION
        //
        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchSupportData()
        {
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.ProductMng.SearchFilterData data = bll.GetFilterData(out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ProductMng.SearchFilterData>() { Data = data, Message = notification, TotalRows = 0 });
        }

        [HttpPost]
        [Route("generatePreparingDataPieceEANCode")]
        public IHttpActionResult GeneratePreparingDataPieceEANCode(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.GeneratePreparingDataPieceEANCode(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("generateEANCode")]
        public IHttpActionResult GenerateEANCode(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.GenerateEANCode(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("generateEANCodeForSET")]
        public IHttpActionResult GenerateEANCodeForSET(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.GenerateEANCodeForSET(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("resetEANCodeForSET")]
        public IHttpActionResult ResetEANCodeForSET(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.ResetEANCodeForSET(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("generatePieceCode")]
        public IHttpActionResult GeneratePieceCode(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.GeneratePieceCode(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        //ean code new feature
        [HttpPost]
        [Route("createSetEanCode")]
        public IHttpActionResult CreateSetEanCode(int productID, string eanCode)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.CreateSetEanCode(productID, eanCode, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        [HttpPost]
        [Route("createColli")]
        public IHttpActionResult CreateColli(int productSetEANCodeID)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.CreateColli(productSetEANCodeID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        [HttpPost]
        [Route("updateColli")]
        public IHttpActionResult UpdateColli(int productColliID, DTO.ProductMng.ProductColli dtoItem)
        {
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));

            object data = bll.UpdateColli(productColliID, dtoItem, out Library.DTO.Notification notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("createColliPiece")]
        public IHttpActionResult CreateColliPiece(int productColliID)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.CreateColliPiece(productColliID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        //ean code feature delete
        [HttpPost]
        [Route("deleteSetEanCode")]
        public IHttpActionResult DeleteSetEanCode(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.DeleteSetEanCode(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        [HttpPost]
        [Route("deleteColli")]
        public IHttpActionResult DeleteColli(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.DeleteColli(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        [HttpPost]
        [Route("deleteColliPiece")]
        public IHttpActionResult DeleteColliPiece(int id)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.DeleteColliPiece(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        [HttpPost]
        [Route("updateColliPiece")]
        public IHttpActionResult UpdateColliPiece(DTO.ProductMng.ProductColliPiece dtoItem)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object item = bll.UpdateColliPiece(dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = item, Message = notification });
        }

        [HttpPost]
        [Route("printEANCode")]
        public IHttpActionResult PrintEANCode(int productColliPieceID)
        {
            Library.DTO.Notification notification;
            BLL.ProductMng bll = new BLL.ProductMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            string reportName = bll.PrintEANCode(productColliPieceID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportName, Message = notification });
        }

        [HttpPost]
        [Route("GetExcelReportData")]
        public IHttpActionResult GetExcelReportData()
        {
            Library.Base.IExecutor executor = Library.Helper.GetDynamicObject("Module.ProductOverviewRpt.Executor, Module.ProductOverviewRpt");
            Module.Framework.BLL bllFramework = new Module.Framework.BLL();

            //return Ok();
            // Check authentication
            if (!bllFramework.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));

            Library.DTO.Notification notification = new Library.DTO.Notification();
            string dataFileName = executor.CustomFunction(ControllerContext.GetAuthUserId(), "ExportExcel", new System.Collections.Hashtable(), out notification).ToString();

            return Ok(new Library.DTO.ReturnData<string> { Data = dataFileName, Message = notification });
        }
    }
}
