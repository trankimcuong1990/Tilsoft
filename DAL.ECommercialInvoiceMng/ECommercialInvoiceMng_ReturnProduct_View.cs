//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ECommercialInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ECommercialInvoiceMng_ReturnProduct_View
    {
        public int ECommercialInvoiceDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ProductSetEANCodeID { get; set; }
        public Nullable<int> ProductColliID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public string ProductEANCode { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> RemaingReturnQnt { get; set; }
        public Nullable<int> ReturnQnt { get; set; }
        public long RowIndex { get; set; }
        public string OrderType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> ReturnColliQnt { get; set; }
        public string ContainerNo { get; set; }
        public string ColliEANCode { get; set; }
        public string ClientUD { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
    }
}