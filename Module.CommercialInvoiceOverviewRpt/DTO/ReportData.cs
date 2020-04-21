using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CommercialInvoiceOverviewRpt.DTO
{
    public class ReportData
    {
        public decimal ExchangeRate { get; set; }
        public List<CommercialInvoice> CommercialInvoices { get; set; }
    }
}
