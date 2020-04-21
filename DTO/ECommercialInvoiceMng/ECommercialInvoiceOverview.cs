using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceOverview
    {
        [Display(Name = "ECommercialInvoiceID")]
        public int? ECommercialInvoiceID { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "RefNo")]
        public string RefNo { get; set; }

        [Display(Name = "InvoiceDate")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "AccountNo")]
        public int? AccountNo { get; set; }

        [Display(Name = "MultiversNo")]
        public string MultiversNo { get; set; }

        [Display(Name = "MVBookingNo")]
        public string MVBookingNo { get; set; }

        [Display(Name = "Conditions")]
        public string Conditions { get; set; }

        [Display(Name = "IsLC")]
        public bool? IsLC { get; set; }

        [Display(Name = "LCRefNo")]
        public string LCRefNo { get; set; }

        [Display(Name = "DeliveryTermID")]
        public int? DeliveryTermID { get; set; }

        [Display(Name = "PaymentTermID")]
        public int? PaymentTermID { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "VATRate")]
        public decimal? VATRate { get; set; }

        [Display(Name = "NetAmount")]
        public decimal? NetAmount { get; set; }

        [Display(Name = "VATAmount")]
        public decimal? VATAmount { get; set; }

        [Display(Name = "TotalAmount")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "DiscountAmount")]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "DiscountDescription")]
        public string DiscountDescription { get; set; }

        [Display(Name = "SeaFreightAmount")]
        public decimal? SeaFreightAmount { get; set; }

        [Display(Name = "SeaFreightDescription")]
        public string SeaFreightDescription { get; set; }

        [Display(Name = "OtherAmount")]
        public decimal? OtherAmount { get; set; }

        [Display(Name = "OtherDescription")]
        public string OtherDescription { get; set; }

        [Display(Name = "ClientAddress")]
        public string ClientAddress { get; set; }

        [Display(Name = "DeliveryTerm")]
        public string DeliveryTerm { get; set; }

        [Display(Name = "PaymentTerm")]
        public string PaymentTerm { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "PrintedBy")]
        public int? PrintedBy { get; set; }

        [Display(Name = "PrintedDate")]
        public DateTime? PrintedDate { get; set; }

        [Display(Name = "IsPrinted")]
        public bool? IsPrinted { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "ConfirmedBy")]
        public int? ConfirmedBy { get; set; }

        [Display(Name = "ConfirmedDate")]
        public DateTime? ConfirmedDate { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "TypeOfInvoice")]
        public int? TypeOfInvoice { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "VATNo")]
        public string VATNo { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "CreditNoteNo")]
        public string CreditNoteNo { get; set; }

        [Display(Name = "DeliveryTypeNM")]
        public string DeliveryTypeNM { get; set; }

        [Display(Name = "PaymentTypeNM")]
        public string PaymentTypeNM { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "PrinterName")]
        public string PrinterName { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "StatusPayment")]
        public string StatusPayment { get; set; }

        public string ClientInvoiceNo { get; set; }

        public string Season { get; set; }

        public int? ParentID { get; set; }

        public int? ParentTypeOfInvoice { get; set; }

        public string IsConfirmedText { get; set; }

        public string TypeOfInvoiceText { get; set; }

        public bool? IsDonePayment { get; set; }

        public string IsDonePaymentText { get; set; }

        public string IsDonePaymentLabel { get; set; }

        public int? BookingID { get; set; }
        public int? CompanyID { get; set; }

        /*
         * FORMATED FIELD
         */
        public string ConcurrencyFlag_String { get; set; }
        public string InvoiceDateFormated { get; set; }
        public string CreatedDateFormated { get; set; }
        public string UpdatedDateFormated { get; set; }
        public string PrintedDateFormated { get; set; }
        public string ConfirmedDateFormated { get; set; }

        /*
         * LIST FIELD
         */
        public List<ECommercialInvoiceDetailOverview> ECommercialInvoiceDetails { get; set; }
        public List<ECommercialInvoiceExtDetailOverview> ECommercialInvoiceExtDetails { get; set; }
        public List<ECommercialInvoiceDescriptionOverview> ECommercialInvoiceDescriptions { get; set; }
        public List<ECommercialInvoiceSparepartDetailOverview> ECommercialInvoiceSparepartDetails { get; set; }
        public List<ECommercialInvoiceContainerTransportOverview> ECommercialInvoiceContainerTransports { get; set; }
        public List<BookingOverview> Bookings { get; set; }
        public List<CreditNoteOverview> CreditNotes { get; set; }
        public List<WarehouseInvoiceProductDetailOverview> WarehouseInvoiceProductDetails { get; set; }
        public List<WarehouseInvoiceSparepartDetailOverview> WarehouseInvoiceSparepartDetails { get; set; }
        public List<WarehouseImportOverview> WarehouseImports { get; set; }


        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public string PrinterName2 { get; set; }
        public string ConfirmerName2 { get; set; }
        public string DefaultCiReport { get; set; }
        public string BLNo { get; set; }
        public string PoLName { get; set; }
    }
}
