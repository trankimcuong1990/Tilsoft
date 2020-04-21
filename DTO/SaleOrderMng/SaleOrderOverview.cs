using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Support;

namespace DTO.SaleOrderMng
{
    public class SaleOrderOverview
    {
        public int? SaleOrderID { get; set; }
        public int? OfferID { get; set; }
        public string Season { get; set; }
        public int? SaleOrderVersion { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Remark { get; set; }
        public bool? IsPIReceived { get; set; }
        public string PIReceivedBy { get; set; }
        public DateTime? PIReceivedDate { get; set; }
        public string PIReceivedRemark { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DeliveryDateInternal { get; set; }
        public DateTime? LDS { get; set; }
        public string Reference { get; set; }
        public string Conditions { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Transportation { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? Commission { get; set; }
        public decimal? VATPercent { get; set; }
        public int? PaymentTermID { get; set; }
        public int? DeliveryTermID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string OrderType { get; set; }
        public int V4ID { get; set; }
        public string V4PINo { get; set; }
        public int? OriginSaleOrderID { get; set; }
        public int? Quantity20DC { get; set; }
        public int? Quantity40DC { get; set; }
        public int? Quantity40HC { get; set; }
        public bool? IsViewQuantityContOnPrintout { get; set; }
        public bool? IsViewDeliveryDateOnPrintout { get; set; }
        public bool? IsViewLDSOnPrintout { get; set; }
        public int? PaymentStatusID { get; set; }
        public string PaymentRemark { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string SignedPIFile { get; set; }
        public string ClientPOFile { get; set; }
        public int? LessThanContainerLoad { get; set; }
        public string ClientOrderNumber { get; set; }

        public string TrackingStatusNM { get; set; }
        public string PaymentTermNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string PIReceiver { get; set; }
        public string OfferUD { get; set; }
        public int TrackingStatusID { get; set; }
        public string PaymentStatusNM { get; set; }

        public bool IsDPReceived { get; set; }
        public bool IsLCReceived { get; set; }
        public bool IsNAReceived { get; set; }
        public string Currency { get; set; }
        public int? ClientPaymentID { get; set; }

        public string SignedPIFileURL { get; set; }
        public string SignedPIFileFriendlyName { get; set; }
        public string ClientPOFileURL { get; set; }
        public string ClientPOFriendlyName { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public int? ClientID { get; set; }
        public int? SumColFactoryOrder { get; set; }

        /*
         * FORMATED FIELD
         */
        public string ConcurrencyFlag_String { get; set; }
        public string InvoiceDateFormated { get; set; }
        public string PIReceivedDateFormated { get; set; }
        public string DeliveryDateFormated { get; set; }
        public string DeliveryDateInternalFormated { get; set; }
        public string LDSFormated { get; set; }
        public string CreatedDateFormated { get; set; }
        public string UpdatedDateFormated { get; set; }
        public string paymentDateFormated { get; set; }

        /*
         * LIST FIELD
         */
        public List<SaleOrderDetailOverView> SaleOrderDetails { get; set; }
        public List<SaleOrderExtendOverView> SaleOrderExtends { get; set; }
        public List<SaleOrderDetailSparepartOverView> SaleOrderDetailSpareparts { get; set; }
        public List<WarehouseImportOverView> WarehouseImports { get; set; }
  
        /*
         * Image Support
         */
        public bool? SignedPIFileHasChange { get; set; }
        public string NewSignedPIFile { get; set; }
        public bool? ClientPOFileHasChange { get; set; }
        public string NewClientPOFile { get; set; }       
        /*
         * Default Template Report
         */
        public string DefaultPiReport { get; set; }

        
    }
}
