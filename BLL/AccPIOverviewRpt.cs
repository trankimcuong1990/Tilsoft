using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AccPIOverviewRpt
    {
        private DAL.AccPIOverviewRpt.DataFactory factory = new DAL.AccPIOverviewRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.AccPIOverviewRpt.ReportData GetHtmlReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get acc pi overview report data - html");

            // query data
            return factory.GetHtmlReportData(season, out notification);
        }

        public string GetExcelReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get acc pi overview report data - excel");

            // query data
            return factory.GetExcelReportData(season, out notification);
        }

        public DTO.AccPIOverviewRpt.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
    }
}
