using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class InvoicePrintout
    {
        public string ClientNM { get; set; }
        public string Address { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string VATNumber { get; set; }
        public string OrderNo { get; set; }
        public string BLNo { get; set; }
        public int? TotalPieces { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? VATPercent { get; set; }
        public decimal? VATAmount { get; set; }
        public string Currency { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Reference { get; set; }
        public string PaymentTermNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string Conditions { get; set; }
        public string SaleNM { get; set; }
    }
}
