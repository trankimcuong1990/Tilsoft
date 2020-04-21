using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search Factory Sale Invoice");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get Factory Sale Invoice");
            return factory.GetData(userId, id, param, out notification);
        }
        public List<DTO.ProductionItemDTO> GetProductionItem(int userId, Hashtable filters)
        {
            return factory.GetProductionItem(userId, filters);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update Factory Sale Invoice" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete Factory Sale Invoice" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }
        public DTO.FactorySaleOrderSearchFromData GetFactorySaleOrderData(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get order item");
            return factory.GetFactorySaleOrderData(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public int SetFactorySaleInvoiceStatus(int userId, int purchaseInvoiceID, int purchaseInvoiceStatusID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "Set factory sale invoice status");
            return factory.SetFactorySaleInvoiceStatus(userId, purchaseInvoiceID, purchaseInvoiceStatusID, out notification);
        }

        public List<DTO.PaymentTermDTO> GetSupplierPaymentTerm(int factoryRawMaterialID, out Library.DTO.Notification notification)
        {
            return factory.GetSupplierPaymentTerm(factoryRawMaterialID, out notification);
        }
    }
}
