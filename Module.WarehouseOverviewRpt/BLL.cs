using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseOverviewRpt
{
    internal class BLL
    {
        private DAL.ReportFactory reportFactory = new DAL.ReportFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public string ExportWarehouseOverview(int iRequesterID, string startDate, string endDate, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, " Get export excel! ");
            return reportFactory.ExportWarehouseOverview(iRequesterID, startDate, endDate, out notification);
        }
    }
}
