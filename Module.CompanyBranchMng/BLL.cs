using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng
{
    internal class BLL
    {
        private readonly DAL.DataFactory dataFactory = new DAL.DataFactory();

        public object GetInitData(int iRequesterID, out Notification notification)
        {
            return dataFactory.GetInitData(iRequesterID, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            return dataFactory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public object GetData(int iRequesterID, int id, Hashtable filters, out Notification notification)
        {
            return dataFactory.GetData(iRequesterID, id, filters, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Notification notification)
        {
            return dataFactory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int iRequesterID, int id, out Notification notification)
        {
            return dataFactory.DeleteData(iRequesterID, id, out notification);
        }

        public object GetQuickSearchBranch(int iRequesterID, Hashtable filters, out Notification notification)
        {
            return dataFactory.GetQuickSearchBranch(filters, out notification);
        }
    }
}
