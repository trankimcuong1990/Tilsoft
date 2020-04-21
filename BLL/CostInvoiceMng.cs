using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class CostInvoiceMng : Lib.BLLBase<DTO.CostInvoiceMng.CostInvoiceSearchResult, DTO.CostInvoiceMng.CostInvoice>
    {
        private DAL.CostInvoiceMng.DataFactory factory = new DAL.CostInvoiceMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.CostInvoiceMng.CostInvoiceSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of eurofar cost invoice");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.CostInvoiceMng.CostInvoice GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get cost invoice " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete eurofar cost invoice " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.CostInvoiceMng.CostInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update cost invoice " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public DTO.CostInvoiceMng.DataContainer GetDataContainer(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get cost invoice " + id.ToString());

            // query data
            return factory.GetDataContainer(id, out notification);
        }

       
        public IEnumerable<DTO.CostInvoiceMng.InitInfo> GetInitInfos(int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list init info");
            return factory.GetInitInfos(out totalRows, out notification);
        }

        public IEnumerable<DTO.CostInvoiceMng.InitInfoDetail> GetInitInfoDetails(int eCommercialInvoiceID,int bookingID, int clientID, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list init info detail");
            return factory.GetInitInfoDetails(eCommercialInvoiceID,bookingID, clientID, out totalRows, out notification);
        }

        public IEnumerable<DTO.CostInvoiceMng.InitInfoDetail> QuickSearchContainer(int eCommercialInvoiceID, int bookingID, int clientID,string containerNo, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list container");
            return factory.QuickSearchContainer(eCommercialInvoiceID,bookingID, clientID,containerNo, out notification);
        }

        public DTO.CostInvoiceMng.InvoiceContainerPrintout GetPrintoutData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get cost invoice printout");
            return factory.GetPrintoutData(id, out  notification);
        }
    }
}
