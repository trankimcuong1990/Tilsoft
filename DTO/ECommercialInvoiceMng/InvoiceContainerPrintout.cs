using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class InvoiceContainerPrintout
    {
        public List<InvoicePrintout> Invoices { get; set; }
        public List<InvoiceDetailPrintout> InvoiceDetails { get; set; }
        public string ReportName { get; set; }
    }
}
