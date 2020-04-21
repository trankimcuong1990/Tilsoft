using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByClientRpt
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.ReportData GetReportData(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(season, out notification);
        }

        public object GetProformaInvoiceConfirmed(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetProformaInvoiceConfirmed(season, out notification);
        }
        public object GetProformaInvoice(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetProformaInvoice(season, out notification);
        }
        public object GetExpectations(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetExpectations(season, out notification);
        }
        public object GetEurofarCommercialInvoices(int iRequesterID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetEurofarCommercialInvoices(season, out notification);
        }        
    }
}
