//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseCIMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseCIMng_WarehouseCIDetail_View
    {
        public int WarehouseCIDetailID { get; set; }
        public Nullable<int> WarehouseCIID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
    
        public virtual WarehouseCIMng_WarehouseCI_View WarehouseCIMng_WarehouseCI_View { get; set; }
    }
}