using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportSalePerCountry
    {
        private BLL.Framework fwBLL = new Framework();
        private DAL.ReportSalePerCountry.DataFactory factory = new DAL.ReportSalePerCountry.DataFactory();

        public string GetReportData(string Season, int SaleID, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get report sale per country");

            // query data
            return factory.GetReportData(Season, SaleID, out notification);
        }

        public DTO.ReportSalePerCountry.SupportDataContainer GetFilters(out Library.DTO.Notification notification)
        {
            return factory.GetSupportData(out notification);
        }
    }
}
