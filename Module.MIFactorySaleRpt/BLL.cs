using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIFactorySaleRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        public DTO.ReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            //// keep log entry
            //fwBLL.WriteLog(iRequesterID, 0, "get sale by item data");

            // query data
            return factory.GetReportData(season, out notification);
        }
    }
}
