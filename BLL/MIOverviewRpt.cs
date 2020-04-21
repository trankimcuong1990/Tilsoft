using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MIOverviewRpt
    {
        private DAL.MIOverviewRpt.DataFactory factory = new DAL.MIOverviewRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.MIOverviewRpt.ReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get mi overview report data");

            // query data
            return factory.GetReportData(season, out notification);
        }
    }
}
