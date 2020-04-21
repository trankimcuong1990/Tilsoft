using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class DraftCommercialInvoiceDTO
    {
        public int DraftCommercialInvoiceID { get; set; }
        public int? ClientID { get; set; }
        public string InvoiceNo { get; set; }
        public string RefNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? AccountNo { get; set; }
        public string Conditions { get; set; }
        public string LCRefNo { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? PaymentTermID { get; set; }
        public string Currency { get; set; }
        public decimal? VATRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ClientAddress { get; set; }
        public string DeliveryTerm { get; set; }
        public string PaymentTerm { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string Remark { get; set; }
        public string ClientInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string VATNo { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string IsConfirmedText { get; set; }
        public List<DraftCommercialInvoiceDetailDTO> DraftCommercialInvoiceDetails
        {
            get; set;
        }
        public List<DraftCommercialInvoiceDescriptionDTO> DraftCommercialInvoiceDescriptions
        {
            get; set;
        }
        public DraftCommercialInvoiceDTO()
        {
            DraftCommercialInvoiceDetails = new List<DraftCommercialInvoiceDetailDTO>();
            DraftCommercialInvoiceDescriptions = new List<DraftCommercialInvoiceDescriptionDTO>();           
        }
    }
}
