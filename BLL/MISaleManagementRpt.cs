using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MISaleManagementRpt
    {
        private DAL.MISaleManagementRpt.DataFactory factory = new DAL.MISaleManagementRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.MISaleManagementRpt.ReportData GetReportData(int iRequesterID, string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(season, saleId, out notification);
        }
        public DTO.MISaleManagementRpt.ReportData GetReportData_ForDeltaOverview(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportData_ForDeltaOverview(season, out notification);
        }
    }
}
