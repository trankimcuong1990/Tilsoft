using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleProformaRpt
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public decimal USDContainerValue { get; set; }
        public decimal EURContainerValue { get; set; }

        public List<AllProformaByMonth> AllProformaByMonths { get; set; }
        public List<ConfirmedContainerPerMonth> ConfirmedContainerPerMonths { get; set; }
        public List<ConfirmedContainerPerSeason> ConfirmedContainerPerSeasons { get; set; }
        public List<ConfirmedProformaByMonth> ConfirmedProformaByMonths { get; set; }
        public List<AllCummulative> AllCummulatives { get; set; }
        public List<ConfirmedCummulative> ConfirmedCummulatives { get; set; }
        public List<EurofarInvoicedCummulative> EurofarInvoicedCummulatives { get; set; }
        public List<EurofarInvoicedPerMonth> EurofarInvoicedPerMonths { get; set; }
    }
}
