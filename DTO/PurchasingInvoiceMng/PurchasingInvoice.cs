using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.PurchasingInvoiceMng
{
    public class PurchasingInvoice
    {
        public int PurchasingInvoiceID { get; set; }

        public string PurchasingInvoiceUD { get; set; }

        public string InvoiceNo { get; set; }

        public bool? IsExpotedToExact { get; set; }

        public string InvoiceDate { get; set; }

        public int? InvoiceType { get; set; }

        public int? BookingID { get; set; }

        public int? SupplierID { get; set; }

        public int? ParentID { get; set; }

        public int? FactoryCostTypeID { get; set; }

        public string Season { get; set; }

        public string FactoryInvoiceNo { get; set; }

        public bool? IsConfirmed { get; set; }

        public int? ConfirmedBy { get; set; }

        public string ConfirmedDate { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public byte[] ConcurrencyFlag { get; set; }

        public string BLNo { get; set; }

        public string ShipedDate { get; set; }

        public string SupplierNM { get; set; }

        public string ForwarderNM { get; set; }

        public string Feeder { get; set; }

        public string POLName { get; set; }

        public string PODName { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public string InvoiceStatus { get; set; }

        public int? UserOfficeID { get; set; }

        public bool? IsConfirmedPrice { get; set; }

        public int? ConfirmedPriceBy { get; set; }

        public string ConfirmedPriceDate { get; set; }
        public int? CompanyID { get; set; }
        public string CompanyNM { get; set; }


        public List<PurchasingInvoiceDetail> PurchasingInvoiceDetails { get; set; }
        public List<PurchasingInvoiceSparepartDetail> PurchasingInvoiceSparepartDetails { get; set; }
        public List<PurchasingCreditNote> PurchasingCreditNotes { get; set; }
        public List<PurchasingInvoiceExtraDetail> PurchasingInvoiceExtraDetails { get; set; }
        public List<DTO.PurchasingInvoiceMng.PurchasingInvoiceSampleDetail> PurchasingInvoiceSampleDetails { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }

        public bool? IsBookingConfirmed { get; set; }
        public int? PackingListID { get; set; }

        public string Remark { get; set; }
        public decimal? DepositPercent { get; set; }
        public int? ForwarderID { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? DepositFactoryAmount { get; set; }
        public decimal? DepositAmountSparepart { get; set; }
        public decimal? DepositFactoryAmountSparepart { get; set; }
        
    }
}