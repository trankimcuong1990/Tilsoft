//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryStockReceipt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryStockReceiptDetail
    {
        public int FactoryStockReceiptDetailID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> ProducedQnt { get; set; }
        public Nullable<int> NotPackedQnt { get; set; }
        public Nullable<int> PackedQnt { get; set; }
    
        public virtual FactoryStockReceipt FactoryStockReceipt { get; set; }
    }
}
