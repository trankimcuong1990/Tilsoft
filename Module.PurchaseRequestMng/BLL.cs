using Library.DTO;
using System.Collections;
using System.Collections.Generic;

namespace Module.PurchaseRequestMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get purchase request list");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete purchase request" + id.ToString());

            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update purchase request" + id.ToString());

            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, Hashtable filters, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get purchase request");
            return factory.GetData(iRequesterID, id, filters, out notification);
        }

        public List<DTO.ProductionItemDTO> GetRequestingItem(int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get purchase request");
            return factory.GetRequestingItem(iRequesterID, out notification);
        }

        public DTO.SearchFormFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public bool ApprovePrice(int iRequesterID, int purchaseRequestDetailID, int purchaseQuotationDetailID, decimal? approvedQnt, out Library.DTO.Notification notification)
        {
            return factory.ApprovePrice(iRequesterID, purchaseRequestDetailID, purchaseQuotationDetailID, approvedQnt, out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }

        public object GetInitData(int userID, out Notification notification)
        {
            return factory.GetInitData(userID, out notification);
        }

        public object GetProductionItemBaseOn(int userID, int? branchID, string workOrderIDs, string searchItem, out Notification notification)
        {
            return factory.GetProductionItemBaseOn(userID, branchID, workOrderIDs, searchItem, out notification);
        }

        public object ReloadItemIsSetGroup(int userID, int? branchID, string productionItemIDs, string workOrderIDs, string stickGroupdIDs, out Notification notification)
        {
            return factory.ReloadItemIsSetGroup(userID, branchID, productionItemIDs, workOrderIDs, stickGroupdIDs, out notification);
        }
    }
}
