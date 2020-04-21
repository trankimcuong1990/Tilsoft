using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class PackingListPrintout
    {
        public string ClientNM { get; set; }
        public string Address { get; set; }
        public string InvoiceDate { get; set; }
        public string OrderNo { get; set; }
        public string BLNo { get; set; }
        public string Reference { get; set; }
        public string DeliveryTermNM { get; set; }
        public int? TotalQuantityShipped { get; set; }
        public int? TotalPKGs { get; set; }
        public decimal? TotalNettWeight { get; set; }
        public decimal? TotalKGs { get; set; }
        public decimal? TotalCBMs { get; set; } 
    }
}
