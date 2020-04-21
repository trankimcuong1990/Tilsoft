using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportCostSummaryRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public string GetReportData(string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(season, out notification);
        }

        public List<Module.Support.DTO.Season> GetInitData(out Library.DTO.Notification notification)
        {
            return factory.GetInitData(out notification);
        }
    }
}
