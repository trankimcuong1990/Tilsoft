using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Library.DTO;

namespace Module.BreakdownPriceListMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "search BreakdownPriceList");
            return factory.GetDataWithFilter(userId, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get BreakdownPriceList");
            return factory.GetData(userId, id, param, out notification);
        }
        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "delete BreakdownPriceList" + id.ToString());
            return factory.DeleteData(userId, id, out notification);
        }
        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "update BreakdownPriceList" + id.ToString());
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }
        public bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve BreakdownPriceList");
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }
        public bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "approve BreakdownPriceList");
            return factory.Reset(userId, id, ref dtoItem, out notification);
        }
        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get init BreakdownPriceList");
            return factory.GetInitData(userId, out notification);
        }
        public object GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(userId, 0, "get search filter BreakdownPriceList");
            return factory.GetInitData(userId, out notification);
        }
        //customize function
        public bool UpdatePrice(int userId, object priceListTable, out Library.DTO.Notification notification)
        {
            return factory.UpdatePrice(userId, priceListTable, out notification);
        }
        public bool AddProductionItemPrice(out Library.DTO.Notification notification)
        {
            return factory.AddProductionItemPrice(out notification);
        }
    }
}
