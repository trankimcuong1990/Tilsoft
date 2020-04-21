using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MISaleByMaterialRpt
    {
        private DAL.MISaleByMaterialRpt.DataFactory factory = new DAL.MISaleByMaterialRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.MISaleByMaterialRpt.ReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get mi sale by material report data");

            // query data
            return factory.GetReportData(season, out notification);
        }
    }
}
