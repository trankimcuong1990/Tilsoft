using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class QuotationOverviewRpt
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.QuotationOverviewRpt.DataFactory factory = new DAL.QuotationOverviewRpt.DataFactory();

        public string GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get quotation overview report");

            // query data
            return factory.GetReportData(iRequesterID, season, out notification);
        }

        public DTO.QuotationOverviewRpt.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

    }
}
