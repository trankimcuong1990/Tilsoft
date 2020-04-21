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
    [RoutePrefix("api/loadingplan")]
    public class LoadingPlanController : ApiController
    {
        private string moduleCode = "LoadingPlanMng";

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

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.LoadingPlanMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int fID, int bID, int pID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            if (id > 0)
            {
                fID = bID = pID = 0;
            }

            DTO.LoadingPlanMng.EditFormData data = bll.GetData(id, fID, bID, pID, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.LoadingPlanMng.LoadingPlan dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.LoadingPlanMng.LoadingPlan>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.LoadingPlan>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.LoadingPlan>() { Data = dtoItem, Message = notification });
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

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("approve")]
        public IHttpActionResult Approve(int id, DTO.LoadingPlanMng.LoadingPlan dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.LoadingPlanMng.LoadingPlan>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.Approve(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.LoadingPlan>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("reset")]
        public IHttpActionResult Reset(int id, DTO.LoadingPlanMng.LoadingPlan dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.LoadingPlanMng.LoadingPlan>(dtoItem, out notification))
            {
                return InternalServerError(new Exception(notification.Message));
            }

            // continue processing
            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.Reset(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.LoadingPlan>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getinitdata")]
        public IHttpActionResult GetInitData()
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            DTO.LoadingPlanMng.InitFormData data = bll.GetInitData(ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.InitFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchproduct")]
        public IHttpActionResult QuicksearchProduct(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.LoadingPlanMng.ProductSearchResult> data = bll.QuicksearchProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<List<DTO.LoadingPlanMng.ProductSearchResult>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("quicksearchsparepart")]
        public IHttpActionResult QuicksearchSparepart(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.LoadingPlanMng.SparepartSearchResult> data = bll.QuicksearchSparepart(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            
            return Ok(new Library.DTO.ReturnData<List<DTO.LoadingPlanMng.SparepartSearchResult>>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("loadingplanstatus")]
        public IHttpActionResult SetLoadingPlanStatus(int id,bool isConfirmed, bool isLoaded, bool isShipped, int branchID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            bll.SetLoadingPlanStatus(id, ControllerContext.GetAuthUserId(), branchID, isConfirmed, isLoaded, isShipped, out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchloadingplan")]
        public IHttpActionResult QuickSearchLoadingPlan(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.LoadingPlanMng.LoadingPlanSearchResult> data = bll.QuickSearchLoadingPlan(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.LoadingPlanMng.LoadingPlanSearchResult>>() { Data = data, Message = notification, TotalRows = totalRows });
        }


        [HttpPost]
        [Route("getreportdata")]
        public IHttpActionResult GetReportData(List<int> loadingPlanIDs, int print_type)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            string reportFileName = bll.GetReportData(loadingPlanIDs, print_type, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = reportFileName, Message = notification });
        }

        [HttpPost]
        [Route("getviewdata")]
        public IHttpActionResult GetViewData(int id, int fID, int bID, int pID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            //if (id > 0)
            //{
            //    fID = bID = pID = 0;
            //}

            DTO.LoadingPlanMng.OverviewData data = bll.GetViewData(id, fID, bID, pID, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.LoadingPlanMng.OverviewData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getPrintOutHTML")]
        public IHttpActionResult GetPrinOutHTML(int loadingPlanID)
        {
            //authentication
            Module.Framework.BLL fwBLL = new Module.Framework.BLL();
            if(!fwBLL.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            Library.DTO.Notification notification = new Library.DTO.Notification();
            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            object data = bll.GetReportHTML(loadingPlanID, out notification);
            return Ok(new Library.DTO.ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchsampleproduct")]
        public IHttpActionResult QuicksearchSampleProduct(DTO.Search searchInput)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.LoadingPlanMng bll = new BLL.LoadingPlanMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.LoadingPlanMng.SampleProductSearchResult> data = bll.QuicksearchSampleProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<List<DTO.LoadingPlanMng.SampleProductSearchResult>>() { Data = data, Message = notification, TotalRows = totalRows });
        }
    }
}
