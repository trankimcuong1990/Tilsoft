using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DocumentClientMng
{
    public class ECommercialInvoice
    {
        public object KeyID { get; set; }
        public int? DocumentClientID { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public string EurofarInvoiceNo { get; set; }
        public string CIReportTemplateNM { get; set; }
    }
}
