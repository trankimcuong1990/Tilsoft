using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DashboardMng
    {
        private DAL.DashboardMng.DataFactory factory = new DAL.DashboardMng.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.DashboardMng.DashboardReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            // query data
            return factory.GetReportData(season, out notification);
        }
    }
}
