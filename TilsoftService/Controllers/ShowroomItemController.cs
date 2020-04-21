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
    [RoutePrefix("api/showroomitem")]
    public class ShowroomItemController : ApiController
    {
        private string moduleCode = "ShowroomItemMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ShowroomItemMng bll = new BLL.ShowroomItemMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ShowroomItemMng.SearchFormData data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShowroomItemMng.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ShowroomItemMng bll = new BLL.ShowroomItemMng();
            Library.DTO.Notification notification;
            DTO.ShowroomItemMng.EditFormData data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShowroomItemMng.EditFormData>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.ShowroomItemMng.ShowroomItem dtoItem)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (id > 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            else if (id == 0 && !fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            // validation            
            if (!Helper.CommonHelper.ValidateDTO<DTO.ShowroomItemMng.ShowroomItem>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.ShowroomItemMng.ShowroomItem>() { Data = dtoItem, Message = notification });
            }
            // save data
            BLL.ShowroomItemMng bll = new BLL.ShowroomItemMng(Helper.AuthHelper.GetCurrentUserFolder(ControllerContext));
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ShowroomItemMng.ShowroomItem>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ShowroomItemMng bll = new BLL.ShowroomItemMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("getexportexcel")]
        public IHttpActionResult GetExportExcelFile()
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ShowroomItemMng bll = new BLL.ShowroomItemMng();
            string dataFileName = bll.GetExportExcelFile(ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("quicksearchsampleproduct")]
        public IHttpActionResult QuickSearchSampleProduct(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.ShowroomItemMng bll = new BLL.ShowroomItemMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.ShowroomItemMng.SampleProduct> data = bll.QuickSearchSampleProduct(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ShowroomItemMng.SampleProduct>>() { Data = data, Message = notification, TotalRows = totalRows });
        }
    }
}