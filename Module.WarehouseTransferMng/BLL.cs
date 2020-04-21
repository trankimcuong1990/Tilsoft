using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.WarehouseTransferMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get warehouse transfer");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete receiving note" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update receiving note" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get receiving note");

            return factory.GetData(iRequesterID, id, out notification);
        }  
        
        public object GetInitForm(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetInitForm(userID, out notification);
        }

        public object ConfirmReceiving(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.ConfirmReceiving(userID, id, out notification);
        }

        public object ConfirmDelivering(int userID, int id, out Library.DTO.Notification notification)
        {
            return factory.ConfirmDelivering(userID, id, out notification);
        }

        public object QuickSearchProductionItem(int userID, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchProductionItem(userID, filters, out notification);
        }

        public DTO.WarehouseTransferPrintoutDTO GetWarehouseTransferPrintoutHTML(int iRequesterID, int warehouseTransferID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "warehouse transfer printout html" + warehouseTransferID.ToString());
            return factory.GetWarehouseTransferPrintoutHTML(warehouseTransferID, out notification);
        }

        public List<DTO.StockQntFromWarehouse> GetStockQntFromWarehouse(int FromFactoryWarehouseID, int ProductionItemID, out Library.DTO.Notification notification)
        {
            return factory.GetStockQntFromWarehouse(FromFactoryWarehouseID, ProductionItemID, out notification);
        }
    }
}
