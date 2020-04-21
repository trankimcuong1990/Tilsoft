using Library.DTO;
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
    [RoutePrefix("api/FactoryOrder")]
    public class FactoryOrderController : ApiController
    {
        private string moduleCode = "FactoryOrderMng";

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

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            int totalRows = 0;
            DTO.FactoryOrderMng.DataSearchContainer searchData = bll.SearchDataContainer(ControllerContext.GetAuthUserId(), searchInput.Filters, searchInput.PageSize, searchInput.PageIndex, searchInput.SortedBy, searchInput.SortedDirection, out totalRows, out notification);
           
            return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.DataSearchContainer>() { Data = searchData, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("get")]
        public IHttpActionResult Get(int id, int saleOrderID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            DTO.FactoryOrderMng.DataContainer FactoryOrderData = bll.GetDataContainer(id, saleOrderID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.DataContainer>() { Data = FactoryOrderData, Message = notification });
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult Update(int id, DTO.FactoryOrderMng.FactoryOrder dtoItem)
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
            if (!Helper.CommonHelper.ValidateDTO<DTO.FactoryOrderMng.FactoryOrder>(dtoItem, out notification))
            {
                return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.FactoryOrder>() { Data = dtoItem, Message = notification });
            }

            // continue processing
            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            bll.UpdateData(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);

            return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.FactoryOrder>() { Data = dtoItem, Message = notification });
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

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            bll.DeleteData(id, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<int>() { Data = id, Message = notification });
        }

        [HttpPost]
        [Route("factoryorders")]
        public IHttpActionResult SearchFactoryOrders(int saleOrderID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead)) // Change CanCreate into CanRead.
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }
            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.FactoryOrderMng.FactoryOrderSearch> factoryOrderDatas = bll.SearchFactoryOrders(saleOrderID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.FactoryOrderMng.FactoryOrderSearch>>() { Data = factoryOrderDatas, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("factoryorderdetails")]
        public IHttpActionResult SearchFactoryOrderDetails(int factoryOrderID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead)) // Change CanCreate into CanRead.
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;

            int totalRows = 0;
            IEnumerable<DTO.FactoryOrderMng.FactoryOrderDetailSearch> factoryOrderDetailDatas = bll.SearchFactoryOrderDetails(factoryOrderID, ControllerContext.GetAuthUserId(), out totalRows, out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.FactoryOrderMng.FactoryOrderDetailSearch>>() { Data = factoryOrderDetailDatas, Message = notification, TotalRows = totalRows });
        }

        [HttpPost]
        [Route("confirm")]
        public IHttpActionResult Confirm(int id, DTO.FactoryOrderMng.FactoryOrder dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            string statusName = string.Empty;
            bll.Confirm(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.FactoryOrder>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("revise")]
        public IHttpActionResult Revise(int id, DTO.FactoryOrderMng.FactoryOrder dtoItem)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanReset))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            bll.Revise(id, ref dtoItem, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.FactoryOrder>() { Data = dtoItem, Message = notification });
        }

        [HttpPost]
        [Route("multi_confirm")]
        public IHttpActionResult MultiConfirm(List<int> ids)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            IEnumerable<int> factoryOrderIDSuccess = bll.MultiConfirm(ids, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<int>>() { Data = factoryOrderIDSuccess, Message = notification });
        }

        [HttpPost]
        [Route("multi_revise")]
        public IHttpActionResult MultiRevise(List<int> ids)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanApprove))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            IEnumerable<int> factoryOrderIDSuccess = bll.MultiRevise(ids, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<int>>() { Data = factoryOrderIDSuccess, Message = notification });
        }

        [HttpPost]
        [Route("multi_delete")]
        public IHttpActionResult MultiDelete(List<int> ids)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanDelete))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            IEnumerable<int> factoryOrderIDSuccess = bll.MultiDelete(ids, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<int>>() { Data = factoryOrderIDSuccess, Message = notification });
        }

        [HttpPost]
        [Route("getqcreport")]
        public IHttpActionResult GetQCReport(int factoryOrderDetailID)
        {
            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;

            IEnumerable<DTO.FactoryOrderMng.QCReport> data = bll.GetQCReport(factoryOrderDetailID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<IEnumerable<DTO.FactoryOrderMng.QCReport>>() { Data = data, Message = notification});
        }

        [HttpPost]
        [Route("getProductColli")]
        public IHttpActionResult GetProductColli(int factoryOrderID)
        {
            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            List<DTO.FactoryOrderMng.ProductColli> data = bll.GetProductColli(factoryOrderID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.FactoryOrderMng.ProductColli>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("createFactoryOrderProductColli")]
        public IHttpActionResult CreateFactoryOrderProductColli(List<DTO.FactoryOrderMng.ProductColli> dtoProductColli)
        {
            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            List<DTO.FactoryOrderMng.ProductColli> data = bll.CreateFactoryOrderProductColli(dtoProductColli, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.FactoryOrderMng.ProductColli>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("getviewdata")]
        public IHttpActionResult GetViewData(int id, int saleOrderID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            DTO.FactoryOrderMng.DataContainerOverView FactoryOrderData = bll.GetViewData(id, saleOrderID, ControllerContext.GetAuthUserId(), out notification);
            return Ok(new Library.DTO.ReturnData<DTO.FactoryOrderMng.DataContainerOverView>() { Data = FactoryOrderData, Message = notification });
        }

        [HttpPost]
        [Route("get-general-breakdown")]
        public IHttpActionResult GetGeneralBreakDown(int id)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            List<DTO.FactoryOrderMng.GeneralBreakDownDTO> data = bll.GetGeneralBreakDown(id, out notification);
            return Ok(new Library.DTO.ReturnData<List<DTO.FactoryOrderMng.GeneralBreakDownDTO>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-client-complaint-data")]
        public IHttpActionResult GetClientComplaintData(int ClientComplaintID)
        {
            // authentication
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            var data = bll.GetClientComplaintData(ClientComplaintID, out notification);
            return Ok(new ReturnData<object>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("exportexcelclientcomplaintitem")]
        public IHttpActionResult ExportExcelClientComplaintItem(int clientComplaintItemID)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            
            var dataFileName = bll.ExportExcelClientComplaintItem(ControllerContext.GetAuthUserId(), clientComplaintItemID, out notification);
            return Ok(new ReturnData<string>() { Data = dataFileName.ToString(), Message = notification });
        }

        [HttpPost]
        [Route("get-sale-order-detail")]
        public IHttpActionResult GetSaleOrderDetail(int saleOrderId)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            List<DTO.FactoryOrderMng.FactoryOrderDetail> data = bll.GetSaleOrderDetail(saleOrderId, out notification);
            return Ok(new ReturnData<List<DTO.FactoryOrderMng.FactoryOrderDetail>>() { Data = data, Message = notification });
        }


        [HttpPost]
        [Route("get-sale-order-detail-sparepart")]
        public IHttpActionResult GetSaleOrderDetailSparepart(int saleOrderId)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail> data = bll.GetSaleOrderSparepartDetail(saleOrderId, out notification);
            return Ok(new ReturnData<List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail>>() { Data = data, Message = notification });
        }

        [HttpPost]
        [Route("get-sale-order-detail-sample")]
        public IHttpActionResult GetSaleOrderDetailSample(int saleOrderId)
        {
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            if (!fwBll.CanPerformAction(ControllerContext.GetAuthUserId(), moduleCode, Library.DTO.ModuleAction.CanRead))
            {
                return InternalServerError(new Exception(Properties.Resources.NOT_AUTHORIZED));
            }

            BLL.FactoryOrderMng bll = new BLL.FactoryOrderMng();
            Library.DTO.Notification notification;
            List<DTO.FactoryOrderMng.FactoryOrderSampleDetail> data = bll.GetSaleOrderSampleDetail(saleOrderId, out notification);
            return Ok(new ReturnData<List<DTO.FactoryOrderMng.FactoryOrderSampleDetail>>() { Data = data, Message = notification });
        }
    }
}
