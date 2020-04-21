using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MISaleByCountryRpt
    {
        private DAL.MISaleByCountryRpt.DataFactory factory = new DAL.MISaleByCountryRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.MISaleByCountryRpt.ReportData GetReportData(int iRequesterID, string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(season, saleId, out notification);
        }

        public DTO.MISaleByCountryRpt.ReportData GetReportForMiDelta(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportForMiDelta(season, out notification);
        }
    }
}
