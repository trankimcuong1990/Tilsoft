//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CostInvoice2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostInvoice2Mng_CostInvoice2Client_View
    {
        public int CostInvoice2ClientID { get; set; }
        public Nullable<int> CostInvoice2ID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<decimal> AmountClient { get; set; }
    
        public virtual CostInvoice2Mng_CostInvoice2_View CostInvoice2Mng_CostInvoice2_View { get; set; }
        public virtual CostInvoice2Mng_CostInvoice2SearchResult_View CostInvoice2Mng_CostInvoice2SearchResult_View { get; set; }
    }
}