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
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/documentmonitoring")]
    public class DocumentMonitoringController : ApiController
    {

        private string moduleCode = "DocumentMonitoringMng";

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

            BLL.DocumentMonitoringMng bll = new BLL.DocumentMonitoringMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.DocumentMonitoringMng.DocumentMonitoringSearch> searchResult = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);

            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.DocumentMonitoringMng.DocumentMonitoringSearch>>() { Data = searchResult, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.DocumentMonitoringMng bll = new BLL.DocumentMonitoringMng();
            Library.DTO.Notification notification;
            DTO.DocumentMonitoringMng.DocumentMonitoring data = bll.GetData(id, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.DocumentMonitoringMng.DocumentMonitoring>() { Data = data, Message = notification });

        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.DocumentMonitoringMng.DocumentMonitoring dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.DocumentMonitoringMng.DocumentMonitoring>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.DocumentMonitoringMng.DocumentMonitoring>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.DocumentMonitoringMng bll = new BLL.DocumentMonitoringMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            
            return Ok(new Library.DTO.ReturnData<DTO.DocumentMonitoringMng.DocumentMonitoring>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("quickupdate")]
        public IHttpActionResult QuickUpdate(List<DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate> dtoItem)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanUpdate))
            {
                // edit case
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            // continue processing
            BLL.DocumentMonitoringMng bll = new BLL.DocumentMonitoringMng();
            bll.QuickUpdateData(ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate>>() { Data = dtoItem, Message = notification });
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

            BLL.DocumentMonitoringMng bll = new BLL.DocumentMonitoringMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("geteditsupport")]
        public IHttpActionResult GetEditSupport()
        {
            BLL.DocumentMonitoringMng bll = new BLL.DocumentMonitoringMng();
            DTO.DocumentMonitoringMng.EditSupportList data = bll.GetEditSupportList();
            return Ok(new Library.DTO.ReturnData<DTO.DocumentMonitoringMng.EditSupportList>() { Data = data});
        }


    }
}
