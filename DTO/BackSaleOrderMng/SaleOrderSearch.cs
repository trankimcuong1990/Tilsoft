using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.BackSaleOrderMng
{
    public class SaleOrderSearch
    {
        public int? SaleOrderID { get; set; }

        public int? OfferID { get; set; }

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

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string TrackingStatusNM { get; set; }

        public string PaymentTermNM { get; set; }

        public string PaymentTypeNM { get; set; }

        public string DeliveryTermNM { get; set; }

        public string DeliveryTypeNM { get; set; }

        public int CreatedBy { get; set; }

        public string CreatorName { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatorName { get; set; }

        public string PIReceiver { get; set; }

        public string OrderType { get; set; }

        public string ClientUD { get; set; }

        public string ClientNM { get; set; }

        public int? SaleID { get; set; }

        public string SaleNM { get; set; }

        public int? BackOrderID { get; set; }

        public string CreateUPFullNm { get; set; }

        public string UpdateUPFullNm { get; set; }
    }
}