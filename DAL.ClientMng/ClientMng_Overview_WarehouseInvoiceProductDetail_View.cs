//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMng_Overview_WarehouseInvoiceProductDetail_View
    {
        public long KeyID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public int GoodsID { get; set; }
        public string GoodsType { get; set; }
        public string CMRNo { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Currency { get; set; }
        public Nullable<int> TypeOfInvoice { get; set; }
    }
}