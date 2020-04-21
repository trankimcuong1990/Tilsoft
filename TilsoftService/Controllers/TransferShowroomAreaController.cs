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
    [RoutePrefix("api/transfershowroomarea")]
    public class TransferShowroomAreaController : ApiController
    {
        private string moduleCode = "TransferShowroomAreaMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.TransferShowroomAreaMng bll = new BLL.TransferShowroomAreaMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> data = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>>() { Data = data, Message = notification, TotalRows = totalRows });
        }


        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>() { Data = dtoItem, Message = notification });
            }
            // save data
            BLL.TransferShowroomAreaMng bll = new BLL.TransferShowroomAreaMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("transfermultiitem")]
        public IHttpActionResult TransferMultiItem(List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> dtoItem)
        {
            Library.DTO.Notification notification;
            List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> errorItems;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            // save data
            BLL.TransferShowroomAreaMng bll = new BLL.TransferShowroomAreaMng();
            bll.TransferMultiItem(dtoItem, ControllerContext.GetAuthUserId(), out errorItems, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>>() { Data = errorItems, Message = notification });
        }
    }
}