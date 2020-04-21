using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientSaleOrder
    {
        public int? SaleOrderID { get; set; }
        public string Season { get; set; }
        public int? SaleOrderVersion { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Remark { get; set; }
        public bool? IsPIReceived { get; set; }
        public string PIReceivedBy { get; set; }
        public string PIReceivedDate { get; set; }
        public string PIReceivedRemark { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryDateInternal { get; set; }
        public string LDS { get; set; }
        public string Reference { get; set; }
        public string Conditions { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Transportation { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? Commission { get; set; }
        public decimal? VATPercent { get; set; }
        public string TrackingStatusNM { get; set; }
        public string PaymentTermNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string DeliveryTypeNM { get; set; }
        public string PIReceiver { get; set; }
        public string OrderType { get; set; }
        public string PaymentDate { get; set; }
        public decimal? TotalOrderAmount { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? ClientID { get; set; }
        public int? OfferID { get; set; }

        public string Currency { get; set; }
        public string PaymentStatusNM { get; set; }
        public string PaymentRemark { get; set; }
        public string ClientOrderNumber { get; set; }
        public string SignedPIFileURL { get; set; }
        public string SignedPIFileFriendlyName { get; set; }
        public string ClientPOFileURL { get; set; }
        public string ClientPOFriendlyName { get; set; }
    }
}
