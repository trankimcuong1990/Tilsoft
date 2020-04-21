using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CashBookReceiptMng
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public object GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetInitData(out Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public object GetData(int userId, int id, out Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int userId, int id, out Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }

        public List<DTO.CashBookPostCostData> UpdatePostCostData(int userId, object dtoItem, out Notification notification)
        {
            return factory.UpdatePostCostData(userId, dtoItem, out notification);
        }

        public bool DeletePostCostData(int userId, int id, out Notification notification)
        {
            return factory.DeletePostCostData(id, out notification);
        }

        public List<DTO.CashBookCostItemData> UpdateCostItemData(int userId, object dtoItem, out Notification notification)
        {
            return factory.UpdateCostItemData(userId, dtoItem, out notification);
        }

        public bool DeleteCostItemData(int userId, int id, out Notification notification)
        {
            return factory.DeleteCostItemData(id, out notification);
        }

        public List<DTO.CashBookCostItemDetailData> UpdateCostItemDetailData(int userId, object dtoItem, out Notification notification)
        {
            return factory.UpdateCostItemDetailData(userId, dtoItem, out notification);
        }

        public bool DeleteCostItemDetailData(int userId, int id, out Notification notification)
        {
            return factory.DeleteCostItemDetailData(id, out notification);
        }

        public string ExportCashBook(int userId, Hashtable filters, out Notification notification)
        {
            return factory.ExportCashBook(filters, out notification);
        }
    }
}
