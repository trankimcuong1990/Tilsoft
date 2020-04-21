using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get purchase order list");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get purchase order");

            return factory.GetData(iRequesterID, id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update purchase order" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete purchase order" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public object GetPurchaseRequest(int userId, Hashtable filters, out Notification notification)
        {
            return factory.GetPurchaseRequest(filters, out notification);
        }

        public object GetPurchaseOrderDetailByPurchaseRequestID(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetPurchaseOrderDetailByPurchaseRequestID(filters, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userId, out Notification notification)
        {
            return factory.GetInitData(userId, out notification);
        }

        public string GetExcelReportData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get PurchaseOrder data to excel");

            // query data
            return factory.GetExcelReportData(id, out notification);
        }
  
        public DTO.PurchaseQuotationData GetPurchaseQuotationDetail(int iRequesterID, int factoryRawMaterialID, DateTime purchaseOrderDate, out Library.DTO.Notification notification)
        {
            return factory.GetPurchaseQuotationDetail(factoryRawMaterialID, purchaseOrderDate, out notification);
        }
        public DTO.PurchaseOrderPrintOutDto GetHTMLReportData(int iRequesterID, int purchaseOrderID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "purchase Order printout" + purchaseOrderID.ToString());
            return factory.GetHTMLReportData(purchaseOrderID, out notification);
        }
        public string GetPurchaseOrderPrintout(int iRequesterID, int deliveryNoteID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "purchase order printout" + deliveryNoteID.ToString());
            return factory.GetPurchaseOrderPrintout(deliveryNoteID, out notification);
        }
        public object GetSupplierPaymentTerm(int userId, int factoryRawMaterialID, out Notification notification)
        {
            return factory.GetSupplierPaymentTerm(factoryRawMaterialID, out notification);
        }
        public bool Cancel(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Cancel(userId, id, ref dtoItem, out notification);
        }
        public bool Finish(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Finish(userId, id, ref dtoItem, out notification);
        }

        public bool Revise(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Revise(userId, id, ref dtoItem, out notification);
        }
    }
}
