using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EurofarPriceOverviewRpt
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportEurofarPriceOverview.DataFactory factory = new DAL.ReportEurofarPriceOverview.DataFactory();

        public string GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get eurofar price overview report");

            // query data
            return factory.GetReportData(season, out notification);
        }

        public DTO.EurofarPriceOverViewRpt.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

    }
}
