namespace Module.TransportCICharge.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransportCIChargeSearchResultDto
    {
        public int TransportCIChargeID { get; set; }
        public string EurofarInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string VATRate { get; set; }
        public string NetAmount { get; set; }
        public string VATAmount { get; set; }
        public string TotalAmount { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? BookingID { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public int? CurrencyID { get; set; }
        public string CurrencyNM { get; set; }
        public int? PaymentTermID { get; set; }
        public string PaymentTermUD { get; set; }
        public string PaymentTermNM { get; set; }
        public int? CreatedBy { get; set; }
        public string CreateFullName { get; set; }
        public string CreateEmployeeNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdateFullName { get; set; }
        public string UpdateEmployeeNM { get; set; }
    }
}
