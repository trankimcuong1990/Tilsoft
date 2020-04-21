using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.ProductionPriceMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get produciton price list");
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "delete produciton price" + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "update produciton price" + id.ToString());
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public DTO.EditFormData GetData(int iRequesterID, int id, Hashtable filters, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get produciton price");
            return factory.GetData(iRequesterID, id, filters, out notification);
        }

        public DTO.SearchFormFilterData GetSearchFilter(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetSearchFilter(out notification);
        }

        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }

        public int? CalculateAvgPrice(int userId, int productionItemTypeID, int month, int year, out Library.DTO.Notification notification)
        {
            return factory.CalculateAvgPrice(userId, productionItemTypeID, month, year, out notification);
        }

        public bool LockPrice(int userId, int id, bool isLocked, out Library.DTO.Notification notification)
        {
            return factory.LockPrice(userId, id, isLocked, out notification);
        }

        public List<DTO.ReceiptByProductionItem> GetReceiptByProductionItem(int userId, int productionItemID, int month, int year, out Library.DTO.Notification notification)
        {
            return factory.GetReceiptByProductionItem(userId, productionItemID, month, year, out notification);
        }

        public int? ApplyPriceOnReceipt(int productionPriceID, out Library.DTO.Notification notification)
        {
            return factory.ApplyPriceOnReceipt(productionPriceID, out notification);
        }

    }
}
