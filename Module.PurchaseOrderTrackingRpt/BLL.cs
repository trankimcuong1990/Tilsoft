using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderTrackingRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public object GetClientWithFilter(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetClientWithFilter(iRequesterID, filters, out notification);
        }

        public object GetWorkOrderWithFilter(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetWorkOrderWithFilter(iRequesterID, filters, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public string GetExcelReportData(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetExcelReportData(filters, out notification);
        }
    }
}
