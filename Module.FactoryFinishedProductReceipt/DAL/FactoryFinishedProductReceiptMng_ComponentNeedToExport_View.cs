//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryFinishedProductReceipt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryFinishedProductReceiptMng_ComponentNeedToExport_View
    {
        public long KeyID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryFinishedProductOrderNormID { get; set; }
        public Nullable<int> FactoryFinishedProductID { get; set; }
        public Nullable<int> FromGoodsProcedureID { get; set; }
        public string StepProgress { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}
