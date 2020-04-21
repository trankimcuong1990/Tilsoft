using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferReportRpt
{
    public class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        public object GetUserFOBItemOnly(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetUserFOBItemOnly(iRequesterID, season, out notification);
        }

        public string GetExcelFOBItemOnlyReportData(string season, out Library.DTO.Notification notification)
        {
            return factory.GetExcelFOBItemOnlyReportData(season, out notification);
        }
    }
}
