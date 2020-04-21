using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class ECommercialInvoiceSearchResult
    {
        [Display(Name = "ECommercialInvoiceID")]
        public int ECommercialInvoiceID { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "RefNo")]
        public string RefNo { get; set; }

        [Display(Name = "InvoiceDate")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "AccountNo")]
        public int AccountNo { get; set; }

        [Display(Name = "MultiversNo")]
        public string MultiversNo { get; set; }

        [Display(Name = "MVBookingNo")]
        public string MVBookingNo { get; set; }

        [Display(Name = "Conditions")]
        public string Conditions { get; set; }

        [Display(Name = "IsLC")]
        public bool IsLC { get; set; }

        [Display(Name = "LCRefNo")]
        public string LCRefNo { get; set; }

        [Display(Name = "Currency")]
        public object Currency { get; set; }

        [Display(Name = "VATRate")]
        public decimal VATRate { get; set; }

        [Display(Name = "NetAmount")]
        public decimal NetAmount { get; set; }

        [Display(Name = "VATAmount")]
        public decimal VATAmount { get; set; }

        [Display(Name = "TotalAmount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "ClientAddress")]
        public object ClientAddress { get; set; }

        [Display(Name = "DeliveryTerm")]
        public string DeliveryTerm { get; set; }

        [Display(Name = "PaymentTerm")]
        public string PaymentTerm { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "PrinterName")]
        public string PrinterName { get; set; }

        [Display(Name = "PrintedDate")]
        public DateTime? PrintedDate { get; set; }

        [Display(Name = "IsPrinted")]
        public bool IsPrinted { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "ConfirmedDate")]
        public DateTime? ConfirmedDate { get; set; }

        [Display(Name = "Remark")]
        public object Remark { get; set; }

        public int? TypeOfInvoice { get; set; }
        public int? CompanyID { get; set; }

        public string IsConfirmedText { get; set; }

        public string TypeOfInvoiceText { get; set; }

        ///
        public string InvoiceDateFormated
        {
            get
            {
                if (InvoiceDate.HasValue)
                    return InvoiceDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string CreatedDateFormated
        {
            get
            {
                if (CreatedDate.HasValue)
                    return CreatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string UpdatedDateFormated
        {
            get
            {
                if (UpdatedDate.HasValue)
                    return UpdatedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string PrintedDateFormated
        {
            get
            {
                if (PrintedDate.HasValue)
                    return PrintedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string ConfirmedDateFormated
        {
            get
            {
                if (ConfirmedDate.HasValue)
                    return ConfirmedDate.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string FactoryInvoiceNo { get; set; }
        public string BLNo { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string PurchasingInvoiceNo { get; set; }
        public string ClientInvoiceNo { get; set; }
        public string Season { get; set; }

        public decimal? NetAmountEUR { get; set; }
        public decimal? VATAmountEUR { get; set; }
        public decimal? TotalAmountEUR { get; set; }
        public decimal? ExRate { get; set; }

    }
}