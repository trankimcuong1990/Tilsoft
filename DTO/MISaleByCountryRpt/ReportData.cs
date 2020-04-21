using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleByCountryRpt
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }

        public List<Summary> Summaries { get; set; }
        public List<ConfirmedSummary> ConfirmedSummaries { get; set; }
        public List<CommercialInvoiceSummary> CommercialInvoiceSummaries { get; set; }
        public List<ExpectedSummary> ExpectedSummaries { get; set; }

        public List<Detail> Details { get; set; }
        public List<ConfirmedDetail> ConfirmedDetails { get; set; }
       
       
    }
}
