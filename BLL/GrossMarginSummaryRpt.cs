using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GrossMarginSummaryRpt
    {
        private DAL.GrossMarginSummaryRpt.DataFactory factory = new DAL.GrossMarginSummaryRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.GrossMarginSummaryRpt.ReportData GetHtmlReportData(int iRequesterID, string fromDate, string toDate, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get gross margin overview report data - html");

            // query data
            return factory.GetHtmlReportData(fromDate, toDate, out notification);
        }

        public string GetExcelReportData(int iRequesterID, string fromDate, string toDate, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get gross margin overview report data - excel");

            // query data
            return factory.GetExcelReportData(fromDate, toDate, out notification);
        }

        public string GetExcelForecastReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetExcelForecastReportData(season, out notification);
        }
    }
}
