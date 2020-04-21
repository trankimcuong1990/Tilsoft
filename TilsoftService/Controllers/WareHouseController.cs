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
    [RoutePrefix("api/warehouse")]
    public class WareHouseController : ApiController
    {
        private string moduleCode = "WareHouseMng";

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

            BLL.WareHouseMng bll = new BLL.WareHouseMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.WareHouseMng.WareHouseSearchResult> WareHouses = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.WareHouseMng.WareHouseSearchResult>>() { Data = WareHouses, Message = notification, TotalRows = totalRows });
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

            BLL.WareHouseMng bll = new BLL.WareHouseMng();
            Library.DTO.Notification notification;
            DTO.WareHouseMng.DataContainer wareHouse = bll.GetDataContainer(id, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.WareHouseMng.DataContainer>() { Data = wareHouse, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.WareHouseMng.WareHouse dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.WareHouseMng.WareHouse>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.WareHouseMng.WareHouse>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.WareHouseMng bll = new BLL.WareHouseMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            if (notification.Type == Library.DTO.NotificationType.Error)
            {
                return InternalServerError(new Exception(notification.Message));
            }
            return Ok(new Library.DTO.ReturnData<DTO.WareHouseMng.WareHouse>() { Data = dtoItem, Message = notification });
        }
    }
}
