using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportPAFF
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportPAFF.DataFactory factory = new DAL.ReportPAFF.DataFactory();

        public string GetReportPAFF(string Season, out Library.DTO.Notification notification)
        {
            return factory.GetReportPAFF(Season, out notification);
        }

        public List<DTO.Support.Season> GetSeasons()
        {
            return factory.GetSeasons();
        }
    }
}
