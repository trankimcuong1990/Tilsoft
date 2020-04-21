using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ECommercialInvoiceMng
{
    public class ReturnProduct
    {
        public int ECommercialInvoiceID { get; set; }
        public int? ProductID { get; set; }
        public int? ProductSetEANCodeID { get; set; }
        public int? ProductColliID { get; set; }
        public int? ECommercialInvoiceDetailID { get; set; }
        public int? ProductStatusID { get; set; }
        public string ProductEANCode { get; set; }
        public int? Quantity { get; set; }
        public int? ReturnQnt { get; set; }
        public int? RemaingReturnQnt { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderID { get; set; }
        public int? SaleOrderID { get; set; }
        public object RowIndex { get; set; }
        public string OrderType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ReturnColliQnt { get; set; }
        public string ContainerNo { get; set; }
        public string ColliEANCode { get; set; }
        public string ClientUD { get; set; }
        public bool? IsSelected { get; set; }
    }
}