using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockOverviewRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();
        public DTO.SearchForm GetDataWithFilters(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(iReiRequesterID, filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }
        public object GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(userId, out notification);
        }
        public string GetExcel( Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetExcel(filters, out notification);
        }
    }
}
