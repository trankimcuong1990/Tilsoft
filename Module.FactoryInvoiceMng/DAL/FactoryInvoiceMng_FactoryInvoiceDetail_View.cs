//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryInvoiceMng_FactoryInvoiceDetail_View
    {
        public int FactoryInvoiceDetailID { get; set; }
        public Nullable<int> FactoryInvoiceID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public string Remark { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ClientUD { get; set; }
    
        public virtual FactoryInvoiceMng_FactoryInvoice_View FactoryInvoiceMng_FactoryInvoice_View { get; set; }
    }
}
