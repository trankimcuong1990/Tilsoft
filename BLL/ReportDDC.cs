using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportDDC
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportDDC.DataFactory factory = new DAL.ReportDDC.DataFactory();

        public string GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(iRequesterID, season, out notification);
        }

        public DTO.ReportDDC.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public DTO.DDCReport.ReportData GetReportData_HTML(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportData_HTML(iRequesterID, season, out notification);
        }
    }
}
