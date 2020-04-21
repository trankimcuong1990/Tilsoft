using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryOrderMng
{
    public class SaleOrderSearch
    {
        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "OfferID")]
        public int? OfferID { get; set; }

        [Display(Name = "Season")]
        public string Season { get; set; }

        [Display(Name = "SaleOrderVersion")]
        public int? SaleOrderVersion { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "InvoiceDate")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "IsPIReceived")]
        public bool? IsPIReceived { get; set; }

        [Display(Name = "PIReceivedBy")]
        public string PIReceivedBy { get; set; }

        [Display(Name = "PIReceivedDate")]
        public DateTime? PIReceivedDate { get; set; }

        [Display(Name = "PIReceivedRemark")]
        public string PIReceivedRemark { get; set; }

        [Display(Name = "DeliveryDate")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "DeliveryDateInternal")]
        public DateTime? DeliveryDateInternal { get; set; }

        [Display(Name = "LDS")]
        public DateTime? LDS { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Conditions")]
        public string Conditions { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Transportation")]
        public decimal? Transportation { get; set; }

        [Display(Name = "CommissionPercent")]
        public decimal? CommissionPercent { get; set; }

        [Display(Name = "Commission")]
        public decimal? Commission { get; set; }

        [Display(Name = "VATPercent")]
        public decimal? VATPercent { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "TrackingStatusNM")]
        public string TrackingStatusNM { get; set; }

        [Display(Name = "PaymentTermNM")]
        public string PaymentTermNM { get; set; }

        [Display(Name = "PaymentTypeNM")]
        public string PaymentTypeNM { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "DeliveryTypeNM")]
        public string DeliveryTypeNM { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "PIReceiver")]
        public string PIReceiver { get; set; }

        [Display(Name = "OfferUD")]
        public string OfferUD { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "SaleID")]
        public int? SaleID { get; set; }

        [Display(Name = "SaleNM")]
        public string SaleNM { get; set; }

        public string InvoiceDateFormated
        {
            get { return (InvoiceDate.HasValue ? InvoiceDate.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string PIReceivedDateFormated
        {
            get { return (PIReceivedDate.HasValue ? PIReceivedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string DeliveryDateFormated
        {
            get { return (DeliveryDate.HasValue ? DeliveryDate.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string DeliveryDateInternalFormated
        {
            get { return (DeliveryDateInternal.HasValue ? DeliveryDateInternal.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string LDSFormated
        {
            get { return (LDS.HasValue ? LDS.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string CreatedDateFormated
        {
            get { return (CreatedDate.HasValue ? CreatedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string UpdatedDateFormated
        {
            get { return (UpdatedDate.HasValue ? UpdatedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

    }
}