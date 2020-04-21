using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FactoryProformaInvoiceMng : Lib.BLLBase2<DTO.FactoryProformaInvoiceMng.SearchFormData, DTO.FactoryProformaInvoiceMng.EditFormData, DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice>
    {
        private DAL.FactoryProformaInvoiceMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();
        public FactoryProformaInvoiceMng(string tempFolder)
        {
            factory = new DAL.FactoryProformaInvoiceMng.DataFactory(tempFolder);
        }

        public override DTO.FactoryProformaInvoiceMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of factory proforma invoice");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.FactoryProformaInvoiceMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete factory proforma invoice " + id.ToString());

            // query data
            return factory.DeleteData(id, iRequesterID, out notification);
        }

        public override bool UpdateData(int id, ref DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update factory proforma invoice " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public DTO.FactoryProformaInvoiceMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }

        public DTO.FactoryProformaInvoiceMng.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public DTO.FactoryProformaInvoiceMng.EditFormData GetData(int id, int iRequesterID, int factoryId, string season, int factoryOrderId, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get factory proforma invoice " + id.ToString());

            return factory.GetData(iRequesterID, id, factoryId, season, factoryOrderId, out notification);
        }

        public IEnumerable<DTO.FactoryProformaInvoiceMng.FactoryOrderItemSearchResult> QuickSearchItem(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchItem(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override bool Approve(int id, ref DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.UpdatedBy = iRequesterID;
            return factory.Approve(id, ref dtoItem, out notification);
        }

        public override bool Reset(int id, ref DTO.FactoryProformaInvoiceMng.FactoryProformaInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            dtoItem.UpdatedBy = iRequesterID;
            return factory.Reset(id, ref dtoItem, out notification);
        }

        public List<DTO.FactoryProformaInvoiceMng.FactoryOrderSearchResult> QuickSearchFactoryOrder(int userId, string season, int factoryId, string query, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchFactoryOrder(userId, season, factoryId, query, out notification);
        }
    }
}
