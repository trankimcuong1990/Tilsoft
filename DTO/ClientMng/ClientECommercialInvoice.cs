using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientECommercialInvoice
    {
        public int? ECommercialInvoiceID { get; set; }
        public string Season { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsPrinted { get; set; }
        public string InvoiceNo { get; set; }
        public string RefNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? AccountNo { get; set; }
        public string MultiversNo { get; set; }
        public string MVBookingNo { get; set; }
        public string Conditions { get; set; }
        public bool? IsLC { get; set; }
        public string LCRefNo { get; set; }
        public string Currency { get; set; }
        public decimal? VATRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ClientAddress { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public string ClientInvoiceNo { get; set; }
        public int? TypeOfInvoice { get; set; }
        public string TypeOfInvoiceText { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? PrintedBy { get; set; }
        public string PrinterName { get; set; }
        public string PrintedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public decimal? NetAmountEUR { get; set; }
        public decimal? VATAmountEUR { get; set; }
        public decimal? TotalAmountEUR { get; set; }
        public string Remark { get; set; }
        public decimal? ExRate { get; set; }
        public int? ClientID { get; set; }
    }
}
