using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportRAPEU
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportRAPEU.DataFactory factory = new DAL.ReportRAPEU.DataFactory();

        public string GetReportRAPEU(bool IsRAPEU,string Season, int FactoryID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report RAPEU");

            // query data
            return factory.GetReportRAPEU(IsRAPEU, Season, FactoryID, iRequesterID, out notification);
        }

        public DTO.ReportRAPEU.SupportDataContainer GetFilters(out Library.DTO.Notification notification)
        {
            return factory.GetSupportData(out notification);
        }
    }
}
