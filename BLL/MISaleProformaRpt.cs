using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MISaleProformaRpt
    {
        private DAL.MISaleProformaRpt.DataFactory factory = new DAL.MISaleProformaRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.MISaleProformaRpt.ReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get mi sale proforma report data");

            // query data
            return factory.GetReportData(season, out notification);
        }
    }
}
