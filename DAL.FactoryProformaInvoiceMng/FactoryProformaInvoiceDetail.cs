//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryProformaInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryProformaInvoiceDetail
    {
        public int FactoryProformaInvoiceDetailID { get; set; }
        public Nullable<int> FactoryProformaInvoiceID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    
        public virtual FactoryProformaInvoice FactoryProformaInvoice { get; set; }
    }
}
