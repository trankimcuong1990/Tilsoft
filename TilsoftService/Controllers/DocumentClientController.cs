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
    [RoutePrefix("api/documentclient")]
    public class DocumentClientController : ApiController
    {

        private string moduleCode = "DocumentClientMng";

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

            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.DocumentClientMng.DataSearchContainer searchResult = bll.SearchDataContainer(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
           
            return Ok(new Library.DTO.ReturnData<DTO.DocumentClientMng.DataSearchContainer>() { Data = searchResult, Message = notification, TotalRows = totalRows });
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

            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            Library.DTO.Notification notification;
            DTO.DocumentClientMng.DataContainer DocumentClient = bll.GetDataContainer(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.DocumentClientMng.DataContainer>() { Data = DocumentClient, Message = notification });

        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.DocumentClientMng.DocumentClient dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.DocumentClientMng.DocumentClient>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.DocumentClientMng.DocumentClient>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            
            
            return Ok(new Library.DTO.ReturnData<DTO.DocumentClientMng.DocumentClient>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("quickupdate")]
        public IHttpActionResult QuickUpdate(List<DTO.DocumentClientMng.DocumentClientSearchUpdate> dtoItem)
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
            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            bll.QuickUpdateData(ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.DocumentClientMng.DocumentClientSearchUpdate>>() { Data = dtoItem, Message = notification });
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

            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("addselectorforeignkey")]
        public IHttpActionResult AddSelectorForeignKey(DTO.DocumentClientMng.ForeignKeyTable dtoItem)
        {
            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            Library.DTO.Notification notification;
            bll.AddSelectorForeignKey(ref dtoItem, out notification);
            
            return Ok(new Library.DTO.ReturnData<DTO.DocumentClientMng.ForeignKeyTable>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("getreport")]
        public IHttpActionResult GetReportData(string season)
        {
            Library.DTO.Notification notification;

            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            string dataFileName = bll.GetReportData(season,ControllerContext.GetAuthUserId(), out notification);
            
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("confirmdatecontainerdelivery")]
        public IHttpActionResult ConfirmDateContainerDelivery(int id, string dateContainerDelivery)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.DocumentClientMng bll = new BLL.DocumentClientMng();
            Library.DTO.Notification notification;
            bll.ConfirmDateContainerDelivery(id,dateContainerDelivery, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }
    }
}
