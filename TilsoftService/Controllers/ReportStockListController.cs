using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TilsoftService.Helper;
using System.Collections;

namespace TilsoftService.Controllers
{
    [Authorize]
    [TilsoftService.Lib.LogFilterAtrribute]
    [RoutePrefix("api/reportstocklist")]
    public class ReportStockListController : ApiController
    {
        private string moduleCode = "ReportStockList";

        [HttpPost]
        [Route("gets")]
        public IHttpActionResult Gets(DTO.Search searchInput)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportStockList bll = new BLL.ReportStockList();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.ReportStockList.SearchFormData data = bll.GetStockListSearch(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out  notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportStockList.SearchFormData>() { Data = data, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("getreportstocklist")]
        public IHttpActionResult GetReportStockList(Hashtable filters)
        {
            Library.DTO.Notification notification;
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanPrint))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.ReportStockList bll = new BLL.ReportStockList(); 
            string dataFileName = bll.GetReportStockList(ControllerContext.GetAuthUserId(), filters, out notification);
            return Ok(new Library.DTO.ReturnData<string>() { Data = dataFileName, Message = notification });
        }

        [HttpPost]
        [Route("activefreetosaleproduct")]
        public IHttpActionResult ActiveFreeToSaleProduct(int productID, bool? isActiveFreeToSale)
        {
            Library.DTO.Notification notification;
            BLL.ReportStockList bll = new BLL.ReportStockList();
            bool data = bll.ActiveFreeToSaleProduct(productID, isActiveFreeToSale, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("setfreetosalecategory")]
        public IHttpActionResult SetFreeToSaleCategory(int productID, int? freeToSaleCategoryID)
        {
            Library.DTO.Notification notification;
            BLL.ReportStockList bll = new BLL.ReportStockList();
            bool data = bll.SetFreeToSaleCategory(productID, freeToSaleCategoryID, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getstockreservation")]
        public IHttpActionResult GetStockReservation(int? goodsID, string productType, int? productStatusID)
        {
            Library.DTO.Notification notification;
            BLL.ReportStockList bll = new BLL.ReportStockList();
            List<DTO.ReportStockList.StockReservation> data = bll.GetStockReservation(goodsID, productType, productStatusID, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.ReportStockList.StockReservation>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("matchedimageproduct")]
        public IHttpActionResult MatchedImageProduct(int productID, bool? isMatchedImage)
        {
            Library.DTO.Notification notification;
            BLL.ReportStockList bll = new BLL.ReportStockList();
            bool data = bll.MatchedImageProduct(productID, isMatchedImage, out notification);
            return Ok(new Library.DTO.ReturnData<bool>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("getstocklistdetail")]
        public IHttpActionResult GetStockListDetail(string keyID)
        {
            Library.DTO.Notification notification;
            BLL.ReportStockList bll = new BLL.ReportStockList();
            DTO.ReportStockList.StockListDetail data = bll.GetStockListDetail(keyID, out notification);
            return Ok(new Library.DTO.ReturnData<DTO.ReportStockList.StockListDetail>() { Data = data, Message = notification });
        }
    }
}
