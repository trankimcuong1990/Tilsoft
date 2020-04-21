using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class FactoryOrderMng : Lib.BLLBase<DTO.FactoryOrderMng.SaleOrderSearch, DTO.FactoryOrderMng.FactoryOrder>
    {
        private DAL.FactoryOrderMng.DataFactory factory = new DAL.FactoryOrderMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.FactoryOrderMng.SaleOrderSearch> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of PI");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.FactoryOrderMng.FactoryOrder GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory order " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete factory order " + id.ToString());
            return factory.DeleteFactoryOrder(id, iRequesterID, out notification);
        }

        public override bool UpdateData(int id, ref DTO.FactoryOrderMng.FactoryOrder dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update factory order " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            dtoItem.UpdatedDate = DateTime.Now;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public DTO.FactoryOrderMng.DataContainer GetDataContainer(int id, int saleOrderID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory order " + id.ToString());

            // query data
            return factory.GetDataContainer(id, saleOrderID, out notification);
        }

        public IEnumerable<DTO.FactoryOrderMng.FactoryOrderSearch> SearchFactoryOrders(int saleOrderID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list factory order by saleorderid");
            return factory.SearchFactoryOrders(saleOrderID, out totalRows, out notification);
        }

        public IEnumerable<DTO.FactoryOrderMng.FactoryOrderDetailSearch> SearchFactoryOrderDetails(int factoryOrderID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list factoryorderdetail by factoryorderid");
            return factory.SearchFactoryOrderDetails(factoryOrderID, out totalRows, out notification);
        }

        public bool Confirm(int id, ref DTO.FactoryOrderMng.FactoryOrder dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "confirm factory order " + id.ToString());
            return factory.Confirm(id, ref dtoItem, iRequesterID, out notification);
        }

        public bool Revise(int id, ref DTO.FactoryOrderMng.FactoryOrder dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "revise factory order " + id.ToString());
            return factory.Revise(id, ref dtoItem, iRequesterID, out notification);
        }

        public DTO.FactoryOrderMng.DataSearchContainer SearchDataContainer(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of PI");
            return factory.SearchDataContainer(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<int> MultiConfirm(List<int> ids, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.MultiConfirm(ids, iRequesterID, out notification);
        }

        public IEnumerable<int> MultiRevise(List<int> ids, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.MultiRevise(ids, iRequesterID, out notification);
        }
        
        public IEnumerable<int> MultiDelete(List<int> ids, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.MultiDelete(ids, iRequesterID, out notification);
        }

        public IEnumerable<DTO.FactoryOrderMng.QCReport> GetQCReport(int factoryOrderDetailID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get qc report");
            return factory.GetQCReport(factoryOrderDetailID, out notification);
        }

        public List<DTO.FactoryOrderMng.ProductColli> GetProductColli(int factoryOrderID, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get product colli");
            return factory.GetProductColli(factoryOrderID, out notification);
        }

        public List<DTO.FactoryOrderMng.ProductColli> CreateFactoryOrderProductColli(List<DTO.FactoryOrderMng.ProductColli> dtoProductColli, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "create product colli");
            return factory.CreateFactoryOrderProductColli(dtoProductColli, out notification);
        }

        public DTO.FactoryOrderMng.DataContainerOverView GetViewData(int id, int saleOrderID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory order over view " + id.ToString());

            // query data
            return factory.GetViewData(id, saleOrderID, out notification);
        }

        public List<DTO.FactoryOrderMng.GeneralBreakDownDTO> GetGeneralBreakDown(int id, out Library.DTO.Notification notification)
        {
            return factory.GetGeneralBreakDown(id, out notification);
        }

        public DTO.FactoryOrderMng.ComplaintData_View GetClientComplaintData(int ClientComplaintID, out Library.DTO.Notification notification)
        {
            return factory.GetClientComplaintData(ClientComplaintID, out notification);
        }


        public string ExportExcelClientComplaintItem(int iRequesterID, int clientComplaintItemID, out Library.DTO.Notification notification)
        {
            return factory.ExportExcelClientComplaintItem(clientComplaintItemID, out notification);
        }


        public List<DTO.FactoryOrderMng.FactoryOrderDetail> GetSaleOrderDetail(int saleOrderId, out Library.DTO.Notification notification)
        {
            return factory.GetSaleOrderDetail(saleOrderId, out notification);
        }

        public List<DTO.FactoryOrderMng.FactoryOrderSparepartDetail> GetSaleOrderSparepartDetail(int saleOrderId, out Library.DTO.Notification notification)
        {
            return factory.GetSaleOrderSparepartDetail(saleOrderId, out notification);
        }
        
        public List<DTO.FactoryOrderMng.FactoryOrderSampleDetail> GetSaleOrderSampleDetail(int saleOrderId, out Library.DTO.Notification notification)
        {
            return factory.GetSaleOrderSampleDetail(saleOrderId, out notification);
        }
    }
}
