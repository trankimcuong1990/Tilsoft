using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt
{
    internal class BLL
    {
        private DAL.DataFactory dataFactory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public object GetInitData(int iRequesterID,int branchID, out Library.DTO.Notification notification)
        {
            return dataFactory.GetInitData(iRequesterID, branchID, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return dataFactory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
    }
}
