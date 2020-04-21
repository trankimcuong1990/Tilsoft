using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LogisticsRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public string GetExcelReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetExcelReportData(season, out notification);
        }
    }
}
