//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryInvoiceOtherMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryInvoiceOtherMng_FactoryInvoiceSearchResult_View
    {
        public int FactoryInvoiceID { get; set; }
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public Nullable<decimal> SubTotalAmount { get; set; }
        public Nullable<decimal> DeductedAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string ConfirmerName { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string FileLocation { get; set; }
    }
}
