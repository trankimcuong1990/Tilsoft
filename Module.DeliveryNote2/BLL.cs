using System.Collections;
using System.Collections.Generic;

namespace Module.DeliveryNote2
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get delivery note list");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete delivery note" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update delivery note" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get delivery note");
            return factory.GetData(iRequesterID, filters, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int branchID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get delivery note");
            return factory.GetData(iRequesterID, id, branchID, out notification);
        }

        public string GetDeliveryNotePrintout(int iRequesterID, int deliveryNoteID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delivery note printout" + deliveryNoteID.ToString());
            return factory.GetDeliveryNotePrintout(deliveryNoteID, out notification);
        }

        public DTO.DeliveryNotePrintoutDTO GetDeliveryNotePrintoutHTML(int iRequesterID, int deliveryNoteID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delivery note printout html" + deliveryNoteID.ToString());
            return factory.GetDeliveryNotePrintoutHTML(deliveryNoteID, out notification);
        }

        public bool DeleteWithPermission(int id, int createdBy, int userId, out Library.DTO.Notification notification)
        {
            return factory.DeleteWithPermission(id, createdBy, userId, out notification);
        }

        public object GetFormView(int userId, int id, out Library.DTO.Notification notification)
        {
            return factory.GetViewData(userId, id, out notification);
        }

        public List<DTO.ProductionItem> GetProductionItem(string searchQuery, int userId, string deliveryNoteDateStr, int branchID, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItem(searchQuery, userId, deliveryNoteDateStr, branchID, out notification);
        }

        public List<DTO.BOMDTO> GetBOM(string workOrderIDs, int branchID, string deliveryNoteDate, out Library.DTO.Notification notification)
        {
            return factory.GetBOM(workOrderIDs, branchID, deliveryNoteDate, out notification);
        }

        public List<DTO.FactorySaleOrderDTO> GetFactorySaleOrder(int userId, string factorySalesOrderUD, out Library.DTO.Notification notification)
        {
            return factory.GetFactorySaleOrder(userId, factorySalesOrderUD, out notification);
        }

        public string GetDeliveryNoteExportToExcelList(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetDeliveryNoteExportToExcelList(userId, filters, out notification);
        }

        public object GetSearchDetail(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchDetail(userID, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get delivery note");
            return factory.GetData(iRequesterID, id, out notification);
        }

        public object PurchaseOrderQuickseach(int userID, string purchaseOrderUD, int branchID, out Library.DTO.Notification notification)
        {
            return factory.PurchaseOrderQuickseach(userID, purchaseOrderUD, branchID, out notification);
        }

        public object PurchaseOrderDetailQuicksearch(int userID, int purchaseOrderID, int branchID, string deliveryNoteDate, out Library.DTO.Notification notification)
        {
            return factory.PurchaseOrderDetailQuicksearch(purchaseOrderID, branchID, deliveryNoteDate, out notification);
        }

        public object FactorySaleOrderDetailQuicksearch(int userID, int factorySaleOrderID, int branchID, string deliveryNoteDate, out Library.DTO.Notification notification)
        {
            return factory.FactorySaleOrderDetailQuicksearch(factorySaleOrderID, branchID, deliveryNoteDate, out notification);
        }
    }
}
