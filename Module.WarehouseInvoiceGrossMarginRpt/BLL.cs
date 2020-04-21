using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseInvoiceGrossMarginRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private DAL.ReportFactory rptFactory = new DAL.ReportFactory();

        public object GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(iRequesterID, out notification);
        }

        public object GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(iRequesterID, filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string ExportExcel(int iRequesterID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return rptFactory.ExportExcel(iRequesterID, filters, out notification);
        }
    }
}
