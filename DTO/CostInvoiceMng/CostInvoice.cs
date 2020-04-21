using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.CostInvoiceMng
{
    public class CostInvoice
    {
        public int? CostInvoiceID { get; set; }

        public int? ClientID { get; set; }

        public int? BookingID { get; set; }

        public string InvoiceNo { get; set; }

        public string InvoiceRefNo { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public string ProformaInvoiceNo { get; set; }

        public string Currency { get; set; }

        public string Condition { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? PaymentTermID { get; set; }

        public int? DeliveryTermID { get; set; }

        public byte[] ConcurrencyFlag { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public string EurofarInvoiceNo { get; set; }

        public string BLNo { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public string DeliveryTypeNM { get; set; }

        public string PaymentTypeNM { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string InvoiceDateFormated { get; set; }

        public string CreatedDateFormated { get; set; }

        public string UpdatedDateFormated { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public List<CostInvoiceDetail> CostInvoiceDetails { get; set; }

        public string CostType { get; set; }

    }
}