using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search Receiving");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get Receiving");
            return factory.GetData(userId, id, param, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete Receiving" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update Receiving" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }
        //
        //customize function
        //
        public DTO.PurchaseOrderSearchFromData GetPurchaseOrderData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get order item");
            return factory.GetPurchaseOrderData(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public List<DTO.ProductionItemDTO> GetProductionItem(int userId, Hashtable filters)
        {
            return factory.GetProductionItem(userId, filters);
        }
        public int SetPurchaseInvoiceStatus(int userId, int purchaseInvoiceID, int purchaseInvoiceStatusID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get order detail item");
            return factory.SetPurchaseInvoiceStatus(userId, purchaseInvoiceID, purchaseInvoiceStatusID, out notification);
        }

        public int Cancel(int userId, int purchaseInvoiceID, int purchaseInvoiceStatusID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "cancel invoice");
            return factory.Cancel(userId, purchaseInvoiceID, purchaseInvoiceStatusID, out notification);
        }

        public List<DTO.PaymentTermDTO> GetSupplierPaymentTerm(int factoryRawMaterialID, out Library.DTO.Notification notification)
        {
            return factory.GetSupplierPaymentTerm(factoryRawMaterialID, out notification);
        }
    }
}
