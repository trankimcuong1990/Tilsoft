//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryEstimateMaterial.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMaterialReceiptDetail
    {
        public int FactoryMaterialReceiptDetailID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryMaterialID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
        public Nullable<int> FactoryAreaID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> FactoryFinishedProductID { get; set; }
    
        public virtual FactoryMaterialReceipt FactoryMaterialReceipt { get; set; }
    }
}
