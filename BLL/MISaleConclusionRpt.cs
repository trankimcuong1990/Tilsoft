using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MISaleConclusionRpt
    {
        private DAL.MISaleConclusionRpt.DataFactory factory = new DAL.MISaleConclusionRpt.DataFactory();
        private Framework fwBLL = new Framework();

        public DTO.MISaleConclusionRpt.ReportData GetReportData(int iRequesterID, string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetReportData(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.ProformaCountry> GetPiByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetPiByCountry(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.ConfirmedProformaCountry> GetPiConfirmedByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetPiConfirmedByCountry(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.CommercialInvoiceCountry> GetCiByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetCiByCountry(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.Delta> GetDeltaByClient(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetDeltaByClient(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.ExpectedCountry> GetExpectedByCountry(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetExpectedByCountry(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.ExpectedByClient> GetExpectedByClient(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetExpectedByClient(season, saleId, out notification);
        }

        public List<DTO.MISaleConclusionRpt.RangeExpected> GetRangeExpected(string season, int? saleId, out Library.DTO.Notification notification)
        {
            return factory.GetRangeExpected(season, saleId, out notification);
        }
    }
}
