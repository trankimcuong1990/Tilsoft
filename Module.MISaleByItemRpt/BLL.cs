using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.ReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sale by item data");

            // query data
            return factory.GetReportData(season, out notification);
        }

        public DTO.ReportData GetReportData_ForDeltaOverview(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get sale by item data");

            // query data
            return factory.GetReportData_ForDeltaOverview(season, out notification);
        }

        public List<DTO.PiConfirmedByItemCategory> GetPiConfirmedByItemCategory(string season, out Library.DTO.Notification notification)
        {
            return factory.GetPiConfirmedByItemCategory(season, out notification);
        }
    }
}
