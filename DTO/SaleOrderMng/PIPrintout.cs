using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class PIPrintout
    {
        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "ClientAddress")]
        public string ClientAddress { get; set; }

        [Display(Name = "ZipCode_City")]
        public string ZipCode_City { get; set; }

        [Display(Name = "ClientCountryNM")]
        public string ClientCountryNM { get; set; }

        [Display(Name = "InvoiceDate")]
        public string InvoiceDate { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "VATNo")]
        public string VATNo { get; set; }

        [Display(Name = "TotalQnt")]
        public int? TotalQnt { get; set; }

        [Display(Name = "NetAmount")]
        public decimal? NetAmount { get; set; }

        [Display(Name = "VATPercent")]
        public decimal? VATPercent { get; set; }

        [Display(Name = "VATAmount")]
        public decimal? VATAmount { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "GrandTotal")]
        public decimal? GrandTotal { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "PaymentTermNM")]
        public string PaymentTermNM { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "Conditions")]
        public string Conditions { get; set; }

        [Display(Name = "SaleNM")]
        public string SaleNM { get; set; }

    }
}