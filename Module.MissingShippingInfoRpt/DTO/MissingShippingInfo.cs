using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MissingShippingInfoRpt.DTO
{
    public class MissingShippingInfo
    {
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string PIReceivedDate { get; set; }
        public string LDS { get; set; }
    }
}
