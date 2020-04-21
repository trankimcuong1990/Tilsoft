using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class Booking
    {
        public Int64? KeyID { get; set; }
        public int? BookingID { get; set; }
        public int? ECommercialInvoiceID { get; set; }
        public string BLNo { get; set; }
        public string ETA { get; set; }
        public string ETA2 { get; set; }
        public string ForwarderInfo { get; set; }
        public string InvoiceNo { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public int? PurchasingInvoiceID { get; set; }
        public string ETD { get; set; }

    }
}
