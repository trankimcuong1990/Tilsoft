namespace Module.WorkOrder
{
    using Library.DTO;
    using System.Collections;
    using System.Collections.Generic;

    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get work order list");
            filters["userId"] = iRequesterID;
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete work order" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update work order" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, int branchID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get work order" + id.ToString());

            return factory.GetData(iRequesterID, id, branchID, out notification);
        }

        public bool Approve(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(iRequesterID, id, ref dtoItem, out notification);
        }

        public List<DTO.ProductDTO> GetProduct(System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetProduct(filters, out notification);
        }

        public List<DTO.SaleOrderDTO> GetSaleOrder(System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetSaleOrder(filters, out notification);
        }

        public List<DTO.FactoryOrderDetailDTO> GetFactoryOrderDetail(int? userId, System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetFactoryOrderDetail(userId, filters, out notification);
        }

        public int? SetWorkOrderStatus(int userId, System.Collections.Hashtable param, out Notification notification)
        {
            return factory.SetWorkOrderStatus(userId, param, out notification);
        }

        public bool ChangeOPSequence(int userId, System.Collections.Hashtable param, out Notification notification)
        {
            return factory.ChangeOPSequence(userId, param, out notification);
        }

        public List<DTO.SampleOrderDTO> GetSampleOrder(int? userId, System.Collections.Hashtable filters, out Notification notification)
        {
            return factory.GetSampleOrder(userId, filters, out notification);
        }

        public List<DTO.PreOrderWorkOrderDTO> GetPreOrderWorkOrder(string workOrderUD, out Notification notification)
        {
            return factory.GetPreOrderWorkOrder(workOrderUD, out notification);
        }

        public List<DTO.WorkCenterDTO> GetWorkCenter(out Notification notification)
        {
            return factory.GetWorkCenter(out notification);
        }

        public string GetProductionItemExportToExcelList(string workOrders, string workCenters, out Library.DTO.Notification notification)
        {
            return factory.GetProductionItemExportToExcelList(workOrders, workCenters, out notification);
        }

        public int? OpenWorkOrderStatus(int userId, System.Collections.Hashtable param, out Notification notification)
        {
            return factory.OpenWorkOrderStatus(userId, param, out notification);
        }

        public object GetOPSequenceDetails(int userId, int? opSequenceID, int? preWorkOrderID, out Notification notification)
        {
            return factory.GetOPSequenceDetails(opSequenceID, preWorkOrderID, out notification);
        }

        public object GetPreOrderWorkOrderManagements(int userID, int preOrderWorkOrderID, out Notification notification)
        {
            return factory.GetPreOrderWorkOrderManagements(preOrderWorkOrderID, out notification);
        }

        public object GetWorkOrderBaseOnManagements(int userID, int workOrderID, int preOrderWorkOrderID, out Notification notification)
        {
            return factory.GetWorkOrderBaseOnManagements(workOrderID, preOrderWorkOrderID, out notification);
        }

        public bool UpdateTransferPre2Work(int userID, Hashtable filters, out Notification notification)
        {
            return factory.UpdateTransferPre2Work(userID, filters, out notification);
        }

        public object GetHistoryTransferPreOrderToWorkOrders(int userID, int workOrderID, int preOrderWorkOrderID, out Notification notification)
        {
            return factory.GetHistoryTransferPreOrderToWorkOrders(workOrderID, preOrderWorkOrderID, out notification);
        }

        public object GetTransferWorkOrder(int userID, int id, int workOrderID, int preOrderWorkOrderID, out Notification notification)
        {
            return factory.GetTransferWorkOrder(id, workOrderID, preOrderWorkOrderID, out notification);
        }
       
    }
}
