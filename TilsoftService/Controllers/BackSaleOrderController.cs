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
    [RoutePrefix("api/backsaleorder")]
    public class BackSaleOrderController : ApiController
    {
        private string moduleCode = "BackSaleOrderMng";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.BackSaleOrderMng bll = new BLL.BackSaleOrderMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.BackSaleOrderMng.SaleOrderSearch> result = bll.GetDataWithFilter(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.BackSaleOrderMng.SaleOrderSearch>>() { Data = result, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getsearchfilter")]
        public IHttpActionResult GetSearchFilter()
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.BackSaleOrderMng bll = new BLL.BackSaleOrderMng();
            DTO.BackSaleOrderMng.SearchFilterData result = bll.GetSearchFilter();
            return Ok(new Library.DTO.ReturnData<DTO.BackSaleOrderMng.SearchFilterData>() { Data = result });
        }

        [HttpPost]
        [Route("getgoodsremaining")]
        public IHttpActionResult GetGoodsRemaining(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanCreate))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.BackSaleOrderMng bll = new BLL.BackSaleOrderMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            IEnumerable<DTO.BackSaleOrderMng.GoodsRemaining> result = bll.GetGoodsRemaining(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);

            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.BackSaleOrderMng.GoodsRemaining>>() { Data = result, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetNewBackOrder(int id)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.BackSaleOrderMng bll = new BLL.BackSaleOrderMng();
            Library.DTO.Notification notification;
            DTO.BackSaleOrderMng.EditFormData result = bll.GetData(id,ControllerContext.GetAuthUserId(),out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BackSaleOrderMng.EditFormData>() { Data = result , Message = notification});
        }

        [HttpPost]
        [Route("update")] //will change on java script
        public IHttpActionResult CreateBackOrder(int id,DTO.BackSaleOrderMng.BackOrder dtoItem)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            BLL.BackSaleOrderMng bll = new BLL.BackSaleOrderMng();
            Library.DTO.Notification notification;
            bll.UpdateBackOrder(id,ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.BackSaleOrderMng.BackOrder>() { Data = dtoItem , Message = notification });
        }

       
    }
}
