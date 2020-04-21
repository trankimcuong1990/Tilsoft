using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng
{
    internal class BLL
    {
        private readonly DAL.DataFactory factory = new DAL.DataFactory();
        private readonly Framework.BLL fwBLL = new Framework.BLL();

        public object GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Get data with filter");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Get data");
            return factory.GetData(filters, out notification);
        }

        public object UpdateData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Update data");
            return factory.UpdateData(iRequesterID, filters, out notification);
        }

        public bool DeleteData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Delete data");
            return factory.DeleteData(iRequesterID, filters, out notification);
        }

        public object ApproveData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Approve data");
            return factory.ApproveData(iRequesterID, filters, out notification);
        }

        public object GetModelSearch(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Search model data");
            return factory.GetModelSearch(iRequesterID, filters, out notification);
        }

        public object GetSampleProductSearch(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Search sample product data");
            return factory.GetSampleProductSearch(iRequesterID, filters, out notification);
        }

        public object GetSupportClient(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetSupportClient(userID, filters, out notification);
        }

        public object GetProductBreakDownCategory(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "Get product break-down category data");
            return factory.GetProductBreakDownCategory(iRequesterID, filters, out notification);
        }
    }
}
