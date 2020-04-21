using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list finished product receipt");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete finished product receipt" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update finished product receipt" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID,int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get finished receipt" + id.ToString());

            return factory.GetData(id, out notification);
        }

        public DTO.ComponentNeedToImports SearchComponentNeedToImport(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "search component need to import");
            return factory.SearchComponentNeedToImport(filters, out notification);
        }
        
        public DTO.ComponentNeedToExports SearchComponentNeedToExport(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "search component need to export");
            return factory.SearchComponentNeedToExport(filters, out notification);
        }

        public DTO.ComponentNeedToImport_WithoutOrders_BuyDirectlies SearchComponentNeedToImportWithoutOrdesBuyDirectly(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "search component need to import without orders buy directly");
            return factory.SearchComponentNeedToImportWithoutOrdesBuyDirectly(filters, out notification);
        }

        public DTO.ComponentNeedToImport_WithoutOrders_InProgresses SearchComponentNeedToImportWithoutOrdesInProgress(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "search component need to import without orders in progress");
            return factory.SearchComponentNeedToImportWithoutOrdesInProgress(filters, out notification);
        }
        public DTO.ComponentNeedToExport_WithoutOrders_InProgresses SearchComponentNeedToExportWithoutOrdesInProgress(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "search component need to export without orders in progress");
            return factory.SearchComponentNeedToExportWithoutOrdesInProgress(filters, out notification);
        }

        public DTO.ComponentNeedToImport_Orders_BuyDirectlies SearchComponentNeedToImportOrdersBuyDirectlies(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "search component need to import by orders directly");
            return factory.SearchComponentNeedToImportOrdersBuyDirectlies(filters, out notification);
        }
        
    }
}
