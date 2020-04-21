using System.Collections;
using System.Collections.Generic;

namespace Module.ReceivingNote
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get receiving note list");
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

        public DTO.EditFormData GetData(int iRequesterID, Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get receiving note");

            return factory.GetData(iRequesterID, filters, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get receiving note");

            return factory.GetData(iRequesterID, id, out notification);
        }

        //public List<DTO.ProductionItem> ReceivingProductionItemFromTeamToWarehouse(int userId, System.Collections.Hashtable filters)
        //{
        //    return factory.ReceivingProductionItemFromTeamToWarehouse(userId, filters);
        //}

        //public List<DTO.ProductionItem> ReceivingProductionItemFromPOToWarehouse(int userId, System.Collections.Hashtable filters)
        //{
        //    return factory.ReceivingProductionItemFromPOToWarehouse(userId, filters);
        //}

        public string GetReceivingNotePrintout(int iRequesterID, int receivingNoteID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "receiving note printout" + receivingNoteID.ToString());
            return factory.GetReceivingNotePrintout(receivingNoteID, out notification);
        }

        public string GetExportBarcode(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetExportBarcode(filters, out notification);
        }

        public DTO.ReceivingNotePrintoutDTO GetReceivingNotePrintoutHTML(int iRequesterID, int receivingNoteID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "receiving note printout" + receivingNoteID.ToString());
            return factory.GetReceivingNotePrintoutHTML(receivingNoteID, out notification);
        }

        public object GetSubSupplier(int userID, string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetSubSupplier(searchQuery, out notification);
        }

        public bool DeleteWithPermission(int id, int createdBy, int userId, out Library.DTO.Notification notification)
        {
            return factory.DeleteWithPermission(id, createdBy, userId, out notification);
        }

        public List<DTO.PurchaseOrderDTO> GetPurchaseOrder(int userId, string purchaseOrderUD, out Library.DTO.Notification notification)
        {
            return factory.GetPurchaseOrder(userId, purchaseOrderUD, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }

        public List<DTO.ProductionItem> GetProductionItem(string searchQuery, int? branchID, string receivingNoteDate, string workOrderIDs, int? workCenterID, int userId, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItem(searchQuery, branchID, receivingNoteDate, workOrderIDs, workCenterID, userId, out notification);
        }

        public List<DTO.BOMDTO> GetBOM(int userID, int branchID, string workOrderIDs, string receivingNoteDate, int? workCenterID, out Library.DTO.Notification notification)
        {
            return factory.GetBOM(userID, branchID, workOrderIDs, receivingNoteDate, workCenterID, out notification);
        }

        public string GetReceivingNoteExportToExcelList(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetReceivingNoteExportToExcelList(userId, filters, out notification);
        }

        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }

        public object GetWorkOrderItems(int userID, int productionItemID, out Library.DTO.Notification notification)
        {
            return factory.GetWorkOrderItems(userID, productionItemID, out notification);
        }

        public object GetPurchaseOrderDetail(int userID, int purchaseOrderID, int branchID, string receivingNoteDate, out Library.DTO.Notification notification)
        {
            return factory.GetPurchaseOrderDetail(purchaseOrderID, branchID, receivingNoteDate, out notification);
        }

        public object SetContentDetail(int userID, int productionItemID, int factoryWarehouseID, out Library.DTO.Notification notification)
        {
            return factory.SetContentDetail(productionItemID, factoryWarehouseID, out notification);
        }

        public object GetBOMProductionItem(int userID, string productionItemUD, string workOrderIDs, int? workCenterID, int? branchID, string receivingNoteDate, out Library.DTO.Notification notification)
        {
            return factory.GetBOMProductionItem(userID, productionItemUD, workOrderIDs, workCenterID, branchID, receivingNoteDate, out notification);
        }

        public object GetSearchDetail(int userID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchDetail(userID, out notification);
        }

        public bool UpdateFactoryProductionMonitiring(int userId, out Library.DTO.Notification notification)
        {
            return factory.UpdateFactoryProductionMonitiring(userId, out notification);
        }

        public List<DTO.FactorySaleOrderDTO> GetFactorySaleOrder(string searchQuery, out Library.DTO.Notification notification)
        {
            return factory.GetFactorySaleOrder(searchQuery, out notification);
        }

        public List<DTO.FactorySaleOrderDetailDTO> GetFactorySaleOrderDetail(int factorySaleOrderID, string receivingNoteDate, out Library.DTO.Notification notification)
        {
            return factory.GetFactorySaleOrderDetail(factorySaleOrderID, receivingNoteDate, out notification);
        }
    }
}
