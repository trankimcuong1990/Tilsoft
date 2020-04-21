using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryOutWardRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();       
        public object GetDataWithFilters(int userId, int? month, int? year,string workOrderStatusNM, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilters(userId, month, year, workOrderStatusNM, out notification);
        }

        public string GetExcelReportData(int userId, int? month, int? year, string workOrderStatusNM, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(userId, month, year, workOrderStatusNM, out notification);
        }
    }
}
