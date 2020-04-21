using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.InitForm GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public DTO.SearchForm GetDataWithFilters(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(iReiRequesterID, filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public string GetExcelReportData(int userId, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(userId, filters, out notification);
        }
    }
}
