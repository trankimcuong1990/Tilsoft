using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Report
    {
        private BLL.Framework fwBLL = new Framework();

        public string GetReportProformaInvoice(string Season, int iRequesterID, out Library.DTO.Notification notification)
        {
            DAL.ReportProformaInvoice.DataFactory factory = new DAL.ReportProformaInvoice.DataFactory();
            fwBLL.WriteLog(iRequesterID, 0, "get report proformainvoice");
            return factory.GetReportProformaInvoice(Season, iRequesterID, out notification);
        }

        public List<DTO.Support.Season> GetSeasons()
        {
            DAL.Support.DataFactory factory = new DAL.Support.DataFactory();
            return factory.GetSeason().ToList();
        }
    }
}
